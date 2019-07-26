using Finchap.Account.Domain.Shared;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure
{
  internal static class MediatorExtension
  {
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, Context ctx)
    {
      var domainEntities = ctx.ChangeTracker
          .Entries<Entity>()
          .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

      var domainEvents = domainEntities
          .SelectMany(x => x.Entity.DomainEvents)
          .ToList();

      domainEntities.ToList()
          .ForEach(entity => entity.Entity.ClearDomainEvents());

      var tasks = domainEvents
          .Select(async (domainEvent) =>
          {
            await mediator.Publish(domainEvent);
          });

      await Task.WhenAll(tasks);
    }
  }
}