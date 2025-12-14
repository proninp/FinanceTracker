using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.App.Infrastructure.EntityFramework;

public sealed class AccountsDbContext(DbContextOptions<AccountsDbContext> options) : DbContext(options), IDbContext
{
    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<AccountType> AccountTypes => Set<AccountType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("accounts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
