namespace Finchap.Account.Infrastructure.Exceptions
{
  public class TransactionalAccountNotFoundException : InfrastructureException
  {
    internal TransactionalAccountNotFoundException(string message)
        : base(message)
    { }
  }
}