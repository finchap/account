using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure.Repositories
{
  public class FinancialInstitutionAccountRepository :
    IFinancialInstitutionAccountRepository
  {
    private Context _context;

    public FinancialInstitutionAccountRepository(Context context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork
    {
      get
      {
        return _context;
      }
    }

    public FIAccount Add(FIAccount fiAccount)
    {
      if (fiAccount.IsTransient())
      {
        return _context.FinancialInstitutionAccounts
            .Add(fiAccount)
            .Entity;
      }
      else
      {
        return fiAccount;
      }
    }

    public Task<FIAccount> GetAsync(string id)
    {
      return _context.FinancialInstitutionAccounts
        .Include(f => f.Accounts)
        .FirstOrDefaultAsync(f => f.Id == id);
    }

    public FIAccount Update(FIAccount fiAccount)
    {
      return _context.FinancialInstitutionAccounts
        .Update(fiAccount)
        .Entity;
    }
  }
}