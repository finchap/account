using Finchap.Account.Application.Queries;
using Finchap.Account.Application.Results;
using Finchap.Account.Domain;
using Finchap.Account.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure.Queries
{
  public class TransactionalAccountQueries : ITransactionalAccountQueries
  {
    private readonly Context _context;

    public TransactionalAccountQueries(Context context)
    {
      _context = context;
    }

    public async Task<TransactionalAccountResult> GetAccounAsynct(string id)
    {
      var domainObject = await _context.TransactionalAccounts.FirstOrDefaultAsync(f => f.Id == id);
      if (domainObject == null)
      {
        throw new TransactionalAccountNotFoundException($"Account {id} not found");
      }

      return ToResult(domainObject);
    }

    public async Task<List<TransactionalAccountResult>> GetAccountsAsync()
    {
      return await _context.TransactionalAccounts.Select(fi => ToResult(fi)).ToListAsync();
    }

    private static TransactionalAccountResult ToResult(TRAccount domainObject)
    {
      return new TransactionalAccountResult
      {
        Description = domainObject.Description,
        AccountNumber = domainObject.AccountNumber,
        AccountType = (Application.Results.AccountType)Enum.Parse(typeof(Application.Results.AccountType), domainObject.AccountType.ToString()),
        FinancialInstitutionAccountId = domainObject.FinancialInstitutionAccountId,
        Id = domainObject.Id,
        UserId = domainObject.UserId,
        LastRefresh = domainObject.LastRefresh,
      };
    }
  }
}