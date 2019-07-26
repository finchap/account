using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using Finchap.Account.Domain.Shared;
using Finchap.Account.Infrastructure.EntityConfigs;
using Finchap.Account.Infrastructure.Repositories;
using MediatR;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure
{
  public class Context : DbContext, IUnitOfWork
  {
    private readonly IMediator _mediator;

    public Context(DbContextOptions<Context> options, IMediator mediator) : base(options)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    private Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<FIAccount> FinancialInstitutionAccounts { get; set; }
    public DbSet<TRAccount> TransactionalAccounts { get; set; }

    public static async Task<string> GetToken(string authority, string resource, string scope)
    {
      var authContext = new AuthenticationContext(authority);
      ClientCredential clientCred = new ClientCredential("ClientId", "ClientSecret");
      AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

      if (result == null)
        throw new InvalidOperationException("Failed to obtain the JWT token");

      return result.AccessToken;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
      // Dispatch Domain Events collection.
      // Choices:
      // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
      // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
      // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
      // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
      await _mediator.DispatchDomainEventsAsync(this);

      // Build the resulting SecretEntities and publish them to the KeyVault. We can live with the potential inconsistencies.
      await GenerateAndUpdateSecretEntities();

      // After executing this line all the changes (from the Command Handler and Domain Event Handlers)
      // performed throught the DbContext will be commited

      var result = await base.SaveChangesAsync();

      return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new TransactionalAccountConfiguration());
      modelBuilder.ApplyConfiguration(new FinancialInstitutionAccountConfiguration());
    }

    private async Task GenerateAndUpdateSecretEntities()
    {
      var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));

      foreach (var fiAccount in ChangeTracker
        .Entries()
        .OfType<FIAccount>()
        .Where(fi => fi.HasUpdatedSecrets))
      {
        var sec = await kv.GetSecretAsync("vault url", fiAccount.SecretNameId);
        var secretEntity = JsonConvert.DeserializeObject<SecretEntity>(sec.Value);

        foreach (var secret in fiAccount.SecretChanges)
        {
          if (secret.TargetSecret == TargetSecret.Password)
          {
            secretEntity.Password = secret.Value as string;
          }
          else if (secret.TargetSecret == TargetSecret.SecurityQuestion)
          {
            if (secret.ChangeType == SecretChangeType.Clear)
            {
              secretEntity.SecurityQuestions.Clear();
            }
            else if (secret.ChangeType == SecretChangeType.Remove)
            {
              secretEntity.SecurityQuestions.Remove(secret.Value as string);
            }
            else if (secret.ChangeType == SecretChangeType.AddOrUpdate)
            {
              var securityQuestion = secret.Value as SecurityQuestion;
              secretEntity.SecurityQuestions[securityQuestion.Question] = securityQuestion.Answer;
            }
          }
        }

        var secretEntityJson = JsonConvert.SerializeObject(secretEntity);
        await kv.SetSecretAsync("vault url", fiAccount.SecretNameId, secretEntityJson);
      }
    }
  }

  public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<Context>
  {
    public Context CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<Context>()
          .UseSqlServer($"Server=.;Initial Catalog=Finchap.Account;Integrated Security=true");

      return new Context(optionsBuilder.Options, new NoMediator());
    }

    private class NoMediator : IMediator
    {
      public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
      {
        return Task.CompletedTask;
      }

      public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
      {
        return Task.FromResult<TResponse>(default(TResponse));
      }

      public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
      {
        return Task.CompletedTask;
      }
    }
  }
}