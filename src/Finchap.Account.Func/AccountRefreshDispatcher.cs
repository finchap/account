using Finchap.Account.Application.Repositories;
using Finchap.Account.Application.Requests;
using Finchap.Account.Application.Requests.RefreshAccount;
using Finchap.Account.Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finchap.Account.Func
{
  public static class AccountRefreshDispatcher
  {
    private static IServiceProvider _serviceProvider;

    static AccountRefreshDispatcher()
    {
      _serviceProvider = Startup.Configure();
    }

    [FunctionName("AccountRefreshDispatcher")]
    public static async Task Run([TimerTrigger(/*"0 0 * * * *"*/"0/15 * * * * *")]TimerInfo myTimer, TraceWriter log)
    {
      var FinancialInstitutionAccountRepository = _serviceProvider.GetService<IFinancialInstitutionAccountRepository>();
      //var transactionalAccountRepository = _serviceProvider.GetService<ITransactionalAccountRepository>();
      var commandPublisher = _serviceProvider.GetService<IRequestPublisher>();

      //var fiAccounts = await FinancialInstitutionAccountRepository.GetAllAsync();
      var fiAccounts = new List<FIAccount>();
      foreach (var fiAccount in fiAccounts)
      {
        var command = new RefreshAccountRequest
        {
          Accounts = fiAccount.Accounts.Select(t => new RefreshAccountRequest.TransactionalAccount { Id = t.Id, UniqueName = t.UniqueName }).ToArray(),
          FinancialInstitutionAccountId = fiAccount.Id,
          Institution = fiAccount.Institution,
          RefreshFrom = fiAccount.Accounts.Min(a => a.LastRefresh),
          UserId = fiAccount.UserId
        };

        await commandPublisher.PublishRequestAsync(command);
      }
    }
  }
}