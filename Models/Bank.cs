public class Bank
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
}