using BankCoreApi.Models;

public class Permission : AuditableEntity
{
    public int Id { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public ICollection<Role>? Roles { get; set; }
}
