using System;
using System.Threading;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
