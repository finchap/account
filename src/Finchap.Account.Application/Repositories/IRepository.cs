using Finchap.Account.Domain.Shared;

namespace Finchap.Account.Application.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
