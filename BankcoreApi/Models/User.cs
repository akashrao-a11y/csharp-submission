using System;
using System.Collections.Generic;

namespace BankCoreApi.Models
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }                       // Primary key
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // ✅ Audit tracking
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Status indicator (active/inactive)
        public bool Status { get; set; } = true;

        // ✅ Relationships
        public ICollection<Role>? Roles { get; set; } = new List<Role>();       // User can have multiple roles
        public ICollection<Account>? Accounts { get; set; } = new List<Account>(); // User can have multiple bank accounts
    }
}
