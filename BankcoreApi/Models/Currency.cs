namespace BankCoreApi.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; } = string.Empty; // e.g. USD, INR
        public string Name { get; set; } = string.Empty;
    }
}
