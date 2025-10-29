namespace BankCoreApi.Models.Dto
{
    public class CreateAccountDto
    {
        public string Type { get; set; }                // Account type: Savings, Current, TermDeposit
        public string AccountNumber { get; set; }       // Unique account number
        public int UserId { get; set; }                 // User linked to account
        public int BankId { get; set; }                 // Bank ID
        public int? BranchId { get; set; }              // Optional branch
        public int? CurrencyId { get; set; }            // Currency reference (optional)
        public string Currency { get; set; } = "INR";   // Currency code (fallback)
        public decimal InitialDeposit { get; set; } = 0; // Initial deposit during creation
        public decimal MinimumBalance { get; set; } = 0; // Minimum required balance
        public bool IsMinor { get; set; } = false;      // Minor account indicator
        public bool IsPOA { get; set; } = false;        // Power of Attorney
    }
}
