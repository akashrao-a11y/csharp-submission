public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!; // "Deposit", "Withdraw", "CloseFee", etc.
    public string Notes { get; set; } = null!;
}
