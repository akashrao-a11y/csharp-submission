using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<SavingsAccount> SavingsAccounts => Set<SavingsAccount>();
    public DbSet<CurrentAccount> CurrentAccounts => Set<CurrentAccount>();
    public DbSet<AccountOwner> AccountOwners => Set<AccountOwner>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPH for Account
        modelBuilder.Entity<Account>()
            .HasDiscriminator<string>("AccountType")
            .HasValue<SavingsAccount>("Savings")
            .HasValue<CurrentAccount>("Current");

        // Composite keys
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<AccountOwner>()
            .HasKey(ao => new { ao.UserId, ao.AccountId });

        // Relationships
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<AccountOwner>()
            .HasOne(ao => ao.User).WithMany(u => u.AccountOwners).HasForeignKey(ao => ao.UserId);
        modelBuilder.Entity<AccountOwner>()
            .HasOne(ao => ao.Account).WithMany(a => a.Owners).HasForeignKey(ao => ao.AccountId);

        // seed small lookup data can be done here or in a separate DbSeed class
    }
}
