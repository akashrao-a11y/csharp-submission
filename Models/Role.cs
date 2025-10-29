using BankCoreApi.Models;

public class Role : AuditableEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<User>? Users { get; set; }
}
