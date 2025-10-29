using BankCoreApi.Models;

public class Branch : AuditableEntity
{
    public int Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public int BankId { get; set; }
    public Bank? Bank { get; set; }

    public ICollection<Account>? Accounts { get; set; }
}
