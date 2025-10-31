using BankCoreApi.Models;

public class Bank : AuditableEntity
{
    public int Id { get; set; }
    public string BankName { get; set; } = string.Empty;

    public ICollection<Branch>? Branches { get; set; }
}
