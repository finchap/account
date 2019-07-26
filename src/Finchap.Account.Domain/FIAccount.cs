using Finchap.Account.Domain.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Finchap.Account.Domain
{
  public partial class FIAccount : Entity, IAggregateRoot
  {
    private readonly IList<TRAccount> _transactionalAccounts;

    public FIAccount(string userId, string id = null)
    {
      Id = id;
      UserId = userId;
      SecretChanges = new List<SecretChange>();
      _transactionalAccounts = new List<TRAccount>();
    }

    public string AccountNumber { get; set; }
    public IReadOnlyCollection<TRAccount> Accounts => new ReadOnlyCollection<TRAccount>(_transactionalAccounts);
    public string Description { get; set; }
    public string FriendlyName { get; set; }
    public bool HasUpdatedSecrets => SecretChanges.Count > 0;
    public string Institution { get; set; }
    public string SecretNameId { get; set; }
    public FIStateEnum State { get; set; }
    public string UserId { get; set; }

    internal string Password { get; private set; }
    internal IList<SecretChange> SecretChanges { get; set; }

    public void AddSecurityQuestion(string question, string answer)
    {
      SecretChanges.Add(new SecretChange
      {
        ChangeType = SecretChangeType.AddOrUpdate,
        TargetSecret = TargetSecret.SecurityQuestion,
        Value = new SecurityQuestion
        {
          Answer = answer,
          Question = question
        }
      });
    }

    public void AddTransactionalAccount(TRAccount account)
    {
      if (_transactionalAccounts.FirstOrDefault(t => t.Id == account.Id) != null)
      {
        _transactionalAccounts.Add(account);
      }
    }

    public void ClearSecurityQuestion()
    {
      SecretChanges.Add(new SecretChange
      {
        ChangeType = SecretChangeType.Clear,
        TargetSecret = TargetSecret.SecurityQuestion
      });
    }

    public void UpdatePassword(string password)
    {
      SecretChanges.Add(new SecretChange
      {
        ChangeType = SecretChangeType.Clear,
        TargetSecret = TargetSecret.SecurityQuestion,
        Value = password
      });
    }
  }
}