using System;

namespace Finchap.Account.Application.Requests.RefreshAccount
{
  public class RefreshAccountRequest : IRequest
  {
    public TransactionalAccount[] Accounts { get; set; }
    public string FinancialInstitutionAccountId { get; set; }
    public string Institution { get; set; }
    public string Name => GetType().Name;
    public DateTimeOffset RefreshFrom { get; set; }
    public string UserId { get; set; }

    public class TransactionalAccount
    {
      public string Id { get; set; }

      public string UniqueName { get; set; }
    }
  }
}