public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    // link to roles if required — many-to-many RolePermissions omitted for brevity
}
