namespace BankCoreApi.Models;

public abstract class AuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public string? ModifiedBy { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}
