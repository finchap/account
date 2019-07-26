using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finchap.Account.Application.Commands.Register
{
    public sealed class RegisterCommand : IRequest<RegisterResult>
    {
        public RegisterCommand(string username, string password, string institution
          , string internalUserId, string description = null, string friendlyName = null)
        {
            Description = description;
            FriendlyName = friendlyName;
            Institution = institution;
            InternalUserId = internalUserId;
            Password = password;
            AccountNumber = username;

            if (string.IsNullOrEmpty(friendlyName))
            {
                FriendlyName = institution;
            }
        }

        public string AccountNumber { get; }
        public string Description { get; }
        public string FriendlyName { get; }
        public string Institution { get; }
        public string InternalUserId { get; }
        public string Password { get; }
    }
}