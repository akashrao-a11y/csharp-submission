public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsBankUser { get; set; } = false; // bank user vs normal user
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<AccountOwner> AccountOwners { get; set; } = new List<AccountOwner>();
}
