using System;

namespace Finchap.Account.Domain.Shared
{
  public class DomainException : Exception
  {
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public DomainException(string businessMessage)
            : base(businessMessage)
    {
    }

    public DomainException()
    {
    }

    protected DomainException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}