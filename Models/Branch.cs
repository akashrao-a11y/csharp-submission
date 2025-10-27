public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IFSC { get; set; } = null!;
    public int BankId { get; set; }
    public Bank Bank { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}