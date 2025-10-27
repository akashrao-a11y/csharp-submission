public abstract class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; } = 0m;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public bool IsClosed { get; set; } = false;  // closed
    public bool IsDeleted { get; set; } = false; // soft delete
    public bool IsTeamAccount { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<AccountOwner> Owners { get; set; } = new List<AccountOwner>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

public class SavingsAccount : Account
{
    public decimal InterestRate { get; set; } = 0.0m;
    public decimal MinimumBalance { get; set; } = 0m;
}

public class CurrentAccount : Account
{
    public decimal OverdraftLimit { get; set; } = 0m;
}
