public static class DbSeed
{
    public static void Initialize(AppDbContext ctx)
    {
        if (ctx.Banks.Any()) return;

        var bank = new Bank { Name = "MyLocalBank" };
        ctx.Banks.Add(bank);

        var branch = new Branch { Name = "Main Branch", IFSC = "MLB0001", Bank = bank };
        ctx.Branches.Add(branch);

        var inr = new Currency { Code = "INR", Name = "Indian Rupee" };
        var usd = new Currency { Code = "USD", Name = "US Dollar" };
        ctx.Currencies.AddRange(inr, usd);

        var roleUser = new Role { Name = "User" };
        var roleTeller = new Role { Name = "Teller" };
        var roleAdmin = new Role { Name = "Admin" };
        ctx.Roles.AddRange(roleUser, roleTeller, roleAdmin);

        var u1 = new User { FirstName = "Akash", LastName = "K", Email = "akash@example.com", IsBankUser = false };
        var u2 = new User { FirstName = "Bank", LastName = "Teller", Email = "teller@bank.local", IsBankUser = true };
        ctx.Users.AddRange(u1, u2);

        ctx.SaveChanges();

        ctx.UserRoles.AddRange(
            new UserRole { UserId = u1.Id, RoleId = roleUser.Id },
            new UserRole { UserId = u2.Id, RoleId = roleTeller.Id }
        );

        var acc1 = new SavingsAccount
        {
            AccountNumber = "SAV1001",
            Balance = 1000,
            BranchId = branch.Id,
            CurrencyId = inr.Id,
            InterestRate = 3.5m,
            MinimumBalance = 500
        };
        ctx.Accounts.Add(acc1);
        ctx.SaveChanges();

        ctx.AccountOwners.Add(new AccountOwner { AccountId = acc1.Id, UserId = u1.Id, OwnerType = OwnerType.Primary });

        ctx.Transactions.Add(new Transaction { AccountId = acc1.Id, Amount = 1000, Type = "InitialDeposit", Notes = "Seed" });

        ctx.SaveChanges();
    }
}
