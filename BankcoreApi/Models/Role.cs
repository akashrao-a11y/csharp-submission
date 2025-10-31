using BankCoreApi.Models;

public class Role : AuditableEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    // 👇 Add this line to make 'role.Name' also valid
    public string Name => RoleName;

    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<User>? Users { get; set; }
}
