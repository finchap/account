using Finchap.Account.Domain;
using Finchap.Account.Domain.Shared;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Repositories
{
    public interface IFinancialInstitutionAccountRepository : IRepository<FIAccount>
    {
        FIAccount Add(FIAccount fiAccount);

        Task<FIAccount> GetAsync(string id);

        FIAccount Update(FIAccount fiAccount);
    }
}