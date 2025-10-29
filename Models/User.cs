namespace BankCoreApi.Models
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }   // ✅ Changed from UserId → Id (matches DbContext and other models)
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // ✅ Navigation properties
        public ICollection<Role>? Roles { get; set; }        // For Role relationship
        public ICollection<Account>? Accounts { get; set; }  // For linked bank accounts
    }
}
