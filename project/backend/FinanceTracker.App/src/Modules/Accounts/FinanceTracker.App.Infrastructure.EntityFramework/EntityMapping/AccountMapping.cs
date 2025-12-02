using FinanceTracker.App.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.App.Infrastructure.EntityFramework.EntityMapping;

/// <summary>
/// Конфигурация маппинга для счетов.
/// </summary>
internal sealed class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("accounts");

        builder
            .HasKey(a => a.Id);

        builder
            .HasOne(a => a.AccountType)
            .WithMany()
            .HasForeignKey(a => a.AccountTypeId);

        builder
            .Property(a => a.Id)
            .HasColumnName("id");

        builder
            .Property(a => a.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder
            .Property(a => a.AccountTypeId)
            .HasColumnName("account_type_id");

        builder
            .Property(a => a.CurrencyId)
            .IsRequired()
            .HasColumnName("currency_id");

        builder
            .Property(a => a.BankId)
            .HasColumnName("bank_id");

        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnName("name");

        builder
            .Property(a => a.CreditLimit)
            .HasColumnName("credit_limit");

        builder
            .Property(a => a.IsIncludeInBalance)
            .IsRequired()
            .HasColumnName("is_include_in_balance");

        builder
            .Property(a => a.IsDefault)
            .IsRequired()
            .HasColumnName("is_default");

        builder
            .Property(a => a.IsArchived)
            .IsRequired()
            .HasColumnName("is_archived");

        // Audit fields mapping
        builder.Property(a => a.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(a => a.UpdatedBy)
            .HasColumnName("updated_by");

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        // Soft delete fields mapping
        builder.Property(a => a.DeletedBy)
            .HasColumnName("deleted_by");

        builder.Property(a => a.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(a => a.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        // Global query filter for soft delete
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
