using System;
using BankCoreApi.Models;

namespace BankCoreApi.Models
{
    public class Account : AuditableEntity
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; } = string.Empty;

        public string AccountType { get; set; } = "Savings"; // Savings / Current / TermDeposit

        public decimal Balance { get; set; } = 0;

        public string Currency { get; set; } = "INR";

        public bool IsClosed { get; set; } = false;

        // Foreign Keys
        public int UserId { get; set; }
        public User? User { get; set; }

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        // Optional specialized fields
        public decimal? InterestRate { get; set; }
        public bool? IsMinor { get; set; }
        public bool? HasPowerOfAttorney { get; set; }
        public decimal? OverdraftLimit { get; set; }
        public decimal? DepositAmount { get; set; }
        public DateTimeOffset? MaturityDate { get; set; }
    }
}
