using Finchap.Account.Application.Results;
using System.Collections.Generic;

namespace Finchap.Account.Application.Commands.Register
{
    public sealed class RegisterResult
    {
        public RegisterResult(FinancialInstitutionAccountResult fiAccount)
        {
            FIAccount = fiAccount;
        }

        public FinancialInstitutionAccountResult FIAccount { get; }
    }
}