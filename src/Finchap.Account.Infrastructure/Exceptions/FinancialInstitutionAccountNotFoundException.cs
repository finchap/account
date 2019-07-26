namespace Finchap.Account.Infrastructure.Exceptions
{
  public class FinancialInstitutionAccountNotFoundException : InfrastructureException
  {
    internal FinancialInstitutionAccountNotFoundException(string message)
        : base(message)
    { }
  }
}