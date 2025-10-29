namespace BankCoreApi.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // Deposit, Withdraw, etc.
        public string? Notes { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Account? Account { get; set; }
    }
}
