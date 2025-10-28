namespace BankCoreApi.Dtos
{
    public class CreateAccountDto
    {
        public int UserId { get; set; }
        public string AccountType { get; set; } = string.Empty; // "Savings" or "Current"
        public string Branch { get; set; } = string.Empty;
        public string Currency { get; set; } = "INR";
        public decimal InitialDeposit { get; set; }
    }
}
