using Finchap.Account.Application.Queries;
using Finchap.Account.Application.Results;
using Finchap.Account.Domain;
using Finchap.Account.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure.Queries
{
  public class FinancialInstitutionQueries : IFinancialInstitutionAccountQueries
  {
    private readonly Context _context;

    public FinancialInstitutionQueries(Context context)
    {
      _context = context;
    }

    public async Task<FinancialInstitutionAccountResult> GetAccountAsync(string id)
    {
      var domainObject = await _context.FinancialInstitutionAccounts.FirstOrDefaultAsync(f => f.Id == id);
      if (domainObject == null)
      {
        throw new FinancialInstitutionAccountNotFoundException($"Account {id} not found");
      }

      return ToResult(domainObject);
    }

    public Task<List<FinancialInstitutionAccountResult>> GetAccountsAsync()
    {
      return _context.FinancialInstitutionAccounts.Select(fi => ToResult(fi)).ToListAsync();
    }

    private static FinancialInstitutionAccountResult ToResult(FIAccount domainObject)
    {
      return new FinancialInstitutionAccountResult
      {
        Description = domainObject.Description,
        Institution = domainObject.Institution,
        UserId = domainObject.UserId,
        Id = domainObject.Id,
        FriendlyName = domainObject.FriendlyName
      };
    }
  }
}