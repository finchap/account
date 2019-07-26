using Finchap.Account.Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Queries
{
    public interface ITransactionalAccountQueries
    {
        Task<TransactionalAccountResult> GetAccounAsynct(string id);

        Task<List<TransactionalAccountResult>> GetAccountsAsync();
    }
}