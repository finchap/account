using Finchap.Account.Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Queries
{
    public interface IFinancialInstitutionAccountQueries
    {
        Task<FinancialInstitutionAccountResult> GetAccountAsync(string id);

        Task<List<FinancialInstitutionAccountResult>> GetAccountsAsync();
    }
}