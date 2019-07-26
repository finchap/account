using System;

namespace Finchap.Account.Application
{
  public class ApplicationException : Exception
  {
    public ApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ApplicationException() : base()
    {
    }

    internal ApplicationException(string businessMessage)
                       : base(businessMessage)
    {
    }

    protected ApplicationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}