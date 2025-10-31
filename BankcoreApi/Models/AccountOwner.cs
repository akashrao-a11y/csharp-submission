namespace BankCoreApi.Models
{
    public enum OwnerType
    {
        Primary,
        Joint,
        POA,
        Minor
    }

    public class AccountOwner
    {
        public int AccountOwnerId { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public OwnerType OwnerType { get; set; }

        public Account? Account { get; set; }
        public User? User { get; set; }
    }
}
