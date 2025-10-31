using System;
using System.Linq;
using BankCoreApi.Data;
using BankCoreApi.Models;

namespace BankCoreApi.Data
{
    public static class DbSeed
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Banks
            if (!context.Banks.Any())
            {
                var bank = new Bank { BankName = "Central Bank of India" };
                context.Banks.Add(bank);
                context.SaveChanges();
            }

            // Seed Branches
            if (!context.Branches.Any())
            {
                var mainBank = context.Banks.First();
                var branch = new Branch
                {
                    BranchName = "Main Branch",
                    Address = "Chennai",
                    BankId = mainBank.Id
                };
                context.Branches.Add(branch);
                context.SaveChanges();
            }

            // Seed Users
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "akash",
                    PasswordHash = "hashedpassword123",
                    Email = "akash@example.com"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            // Seed Accounts
            if (!context.Accounts.Any())
            {
                var branch = context.Branches.First();
                var user = context.Users.First();

                var account = new Account
                {
                    AccountNumber = "ACC1001",
                    AccountType = "Savings",
                    Balance = 5000,
                    Currency = "INR",
                    BranchId = branch.Id,
                    UserId = user.Id
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            // Seed Currencies
            if (!context.Currencies.Any())
            {
                var currencies = new[]
                {
                    new Currency { Code = "INR", Name = "Indian Rupee" },
                    new Currency { Code = "USD", Name = "US Dollar" },
                    new Currency { Code = "EUR", Name = "Euro" }
                };
                context.Currencies.AddRange(currencies);
                context.SaveChanges();
            }
        }
    }
}
