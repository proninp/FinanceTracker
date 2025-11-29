using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Domain.Constants;
using FinanceTracker.App.ShareKernel.Domain.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.App.Infrastructure.EntityFramework.EntityMapping;

/// <summary>
/// Конфигурация маппинга для переводов типов счетов.
/// </summary>
internal sealed class AccountTypeTranslationMappingExample : IEntityTypeConfiguration<AccountTypeTranslation>
{
    public void Configure(EntityTypeBuilder<AccountTypeTranslation> builder)
    {
        builder
            .ToTable("account_type_translations");

        builder
            .HasKey(t => new { t.EntityId, t.LanguageCode });

        builder
            .Property(t => t.EntityId)
            .HasColumnName("account_type_id")
            .IsRequired();

        builder
            .Property(t => t.LanguageCode)
            .HasColumnName("language_code")
            .HasMaxLength(5)
            .IsRequired();

        builder
            .Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .HasOne(t => t.AccountType)
            .WithMany(at => at.Translations)
            .HasForeignKey(t => t.EntityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(t => t.LanguageCode);

        SeedTranslations(builder);
    }

    private static void SeedTranslations(EntityTypeBuilder<AccountTypeTranslation> builder)
    {
        builder.HasData(
            // BANK - Переводы
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.BankAccountTypeId,
                LanguageCode = LanguageCode.Russian,
                Description = "Банковский счёт"
            },
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.BankAccountTypeId,
                LanguageCode = LanguageCode.English,
                Description = "Bank Account"
            },

            // CASH - Переводы
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CashAccountTypeId,
                LanguageCode = LanguageCode.Russian,
                Description = "Наличные"
            },
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CashAccountTypeId,
                LanguageCode = LanguageCode.English,
                Description = "Cash"
            },

            // CARD - Переводы
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CardAccountTypeId,
                LanguageCode = LanguageCode.Russian,
                Description = "Карта"
            },
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CardAccountTypeId,
                LanguageCode = LanguageCode.English,
                Description = "Card"
            },

            // CREDIT - Переводы
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CreditAccountTypeId,
                LanguageCode = LanguageCode.Russian,
                Description = "Кредит"
            },
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.CreditAccountTypeId,
                LanguageCode = LanguageCode.English,
                Description = "Credit"
            },

            // DEPOSIT - Переводы
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.DepositAccountTypeId,
                LanguageCode = LanguageCode.Russian,
                Description = "Депозит"
            },
            new AccountTypeTranslation
            {
                EntityId = SystemIdentifiers.DepositAccountTypeId,
                LanguageCode = LanguageCode.English,
                Description = "Deposit"
            }
        );
    }
}
