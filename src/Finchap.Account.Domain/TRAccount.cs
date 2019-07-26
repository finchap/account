using Finchap.Account.Domain.Shared;
using System;

namespace Finchap.Account.Domain
{
  public class TRAccount : Entity
  {
    public TRAccount(string id, string userId)
    {
      Id = id;
      UserId = userId;
    }

    public string AccountNumber { get; set; }
    public AccountType AccountType { get; set; }
    public string Description { get; set; }
    public string FinancialInstitutionAccountId { get; set; }

    public DateTimeOffset LastRefresh { get; set; } = DateTimeOffset.UtcNow;
    public string UniqueName => $"{AccountNumber}-{AccountType}";

    public string UserId { get; }
  }
}