using MediatR;
using System;
using System.Collections.Generic;

namespace Finchap.Account.Domain.Shared
{
  public abstract class Entity
  {
    private List<INotification> _domainEvents;

    private int? _requestedHashCode;

    protected Entity()
    {
      Id = Guid.NewGuid().ToString();
    }

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    public virtual string Id { get; protected set; }

    public static bool operator !=(Entity left, Entity right)
    {
      return !(left == right);
    }

    public static bool operator ==(Entity left, Entity right)
    {
      if (Object.Equals(left, null))
        return (Object.Equals(right, null)) ? true : false;
      else
        return left.Equals(right);
    }

    public void AddDomainEvent(INotification eventItem)
    {
      _domainEvents = _domainEvents ?? new List<INotification>();
      _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
      _domainEvents?.Clear();
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is Entity))
        return false;

      if (Object.ReferenceEquals(this, obj))
        return true;

      if (this.GetType() != obj.GetType())
        return false;

      Entity item = (Entity)obj;

      if (item.IsTransient() || this.IsTransient())
        return false;
      else
        return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
      if (!IsTransient())
      {
        if (!_requestedHashCode.HasValue)
          _requestedHashCode = this.Id.GetHashCode();

        return _requestedHashCode.Value;
      }
      else
        return base.GetHashCode();
    }

    public bool IsTransient()
    {
      return string.IsNullOrEmpty(Id);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
      _domainEvents?.Remove(eventItem);
    }
  }
}