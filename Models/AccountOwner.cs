public enum OwnerType { Primary, Minor, POA, Joint }

public class AccountOwner
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public OwnerType OwnerType { get; set; }
}
