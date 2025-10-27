public class Currency
{
    public int Id { get; set; }
    public string Code { get; set; } = null!; // e.g. INR, USD
    public string Name { get; set; } = null!;
}
