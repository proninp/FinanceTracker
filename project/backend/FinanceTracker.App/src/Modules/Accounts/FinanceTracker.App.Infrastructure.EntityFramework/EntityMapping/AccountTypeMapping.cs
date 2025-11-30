using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.App.Infrastructure.EntityFramework.EntityMapping;

/// <summary>
/// Конфигурация маппинга для типов счетов.
/// </summary>
internal sealed class AccountTypeMapping : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder
            .ToTable("account_type");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(at => at.Code)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("code");

        builder
            .Property(at => at.Description)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("description");

        // Audit fields mapping
        builder.Property(at => at.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(at => at.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(at => at.UpdatedBy)
            .HasColumnName("updated_by");

        builder.Property(at => at.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        // Soft delete fields mapping
        builder.Property(at => at.DeletedBy)
            .HasColumnName("deleted_by");

        builder.Property(at => at.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(at => at.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        // Seed data
        SeedAccountTypes(builder);
    }

    private static void SeedAccountTypes(EntityTypeBuilder<AccountType> builder)
    {
        var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new AccountType
            {
                Id = SystemIdentifiers.BankAccountTypeId,
                Code = "BANK",
                Description = "Банковский счёт",
                IsArchived = false,
                CreatedBy = SystemIdentifiers.SystemUserId,
                CreatedAt = seedDate,
                UpdatedBy = null,
                UpdatedAt = seedDate,
                DeletedBy = null,
                DeletedAt = null,
                IsDeleted = false
            },
            new AccountType
            {
                Id = SystemIdentifiers.CashAccountTypeId,
                Code = "CASH",
                Description = "Наличные",
                IsArchived = false,
                CreatedBy = SystemIdentifiers.SystemUserId,
                CreatedAt = seedDate,
                UpdatedBy = null,
                UpdatedAt = seedDate,
                DeletedBy = null,
                DeletedAt = null,
                IsDeleted = false
            },
            new AccountType
            {
                Id = SystemIdentifiers.CardAccountTypeId,
                Code = "CARD",
                Description = "Карта",
                IsArchived = false,
                CreatedBy = SystemIdentifiers.SystemUserId,
                CreatedAt = seedDate,
                UpdatedBy = null,
                UpdatedAt = seedDate,
                DeletedBy = null,
                DeletedAt = null,
                IsDeleted = false
            },
            new AccountType
            {
                Id = SystemIdentifiers.CreditAccountTypeId,
                Code = "CREDIT",
                Description = "Кредит",
                IsArchived = false,
                CreatedBy = SystemIdentifiers.SystemUserId,
                CreatedAt = seedDate,
                UpdatedBy = null,
                UpdatedAt = seedDate,
                DeletedBy = null,
                DeletedAt = null,
                IsDeleted = false
            },
            new AccountType
            {
                Id = SystemIdentifiers.DepositAccountTypeId,
                Code = "DEPOSIT",
                Description = "Депозит",
                IsArchived = false,
                CreatedBy = SystemIdentifiers.SystemUserId,
                CreatedAt = seedDate,
                UpdatedBy = null,
                UpdatedAt = seedDate,
                DeletedBy = null,
                DeletedAt = null,
                IsDeleted = false
            }
        );
    }
}
