using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finchap.Account.Application.Commands.Refresh
{
  public sealed class RefreshCommand : IRequest<bool>
  {
    public RefreshCommand(string userId, string fiAccountId)
    {
      UserId = userId;
      FIAccountId = fiAccountId;
    }

    public string FIAccountId { get; }
    public string UserId { get; }
  }
}