using System;

namespace Finchap.Account.Application.Results
{
  public class TransactionalAccountResult
  {
    public string AccountNumber { get; set; }
    public AccountType AccountType { get; set; }
    public string Description { get; set; }
    public string FinancialInstitutionAccountId { get; set; }
    public string Id { get; set; }
    public DateTimeOffset LastRefresh { get; set; } = DateTimeOffset.UtcNow;
    public string UserId { get; set; }
  }
}