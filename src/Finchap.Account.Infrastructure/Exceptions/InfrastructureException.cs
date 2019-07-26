using System;

namespace Finchap.Account.Infrastructure.Exceptions
{
  public class InfrastructureException : Exception
  {
    internal InfrastructureException(string businessMessage)
           : base(businessMessage)
    {
    }
  }
}