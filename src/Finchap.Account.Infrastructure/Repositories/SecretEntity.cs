using System;
using System.Collections.Generic;
using System.Text;

namespace Finchap.Account.Infrastructure.Repositories
{
  public class SecretEntity
  {
    public string Password { get; set; }
    public Dictionary<string, string> SecurityQuestions { get; set; }
  }
}