namespace Finchap.Account.Domain
{
  public enum SecretChangeType
  {
    Unknown,
    AddOrUpdate,
    Remove,
    Clear,
  }

  public enum TargetSecret
  {
    Unknown,
    Password,
    SecurityQuestion
  }

  public class SecretChange
  {
    public SecretChangeType ChangeType { get; set; }
    public TargetSecret TargetSecret { get; set; }
    public object Value { get; set; }
  }

  public class SecurityQuestion
  {
    public string Answer { get; set; }
    public string Question { get; set; }
  }
}