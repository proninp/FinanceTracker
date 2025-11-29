using FinanceTracker.App.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.App.Infrastructure.EntityFramework.EntityMapping;

/// <summary>
/// Конфигурация маппинга для переводов счетов.
/// </summary>
internal sealed class AccountTranslationMapping : IEntityTypeConfiguration<AccountTranslation>
{
    public void Configure(EntityTypeBuilder<AccountTranslation> builder)
    {
        builder
            .ToTable("account_translations");

        builder
            .HasKey(t => new { t.EntityId, t.LanguageCode });

        builder
            .Property(t => t.EntityId)
            .HasColumnName("account_id")
            .IsRequired();

        builder
            .Property(t => t.LanguageCode)
            .HasColumnName("language_code")
            .HasMaxLength(5)
            .IsRequired();

        builder
            .Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasOne(t => t.Account)
            .WithMany(a => a.Translations)
            .HasForeignKey(t => t.EntityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(t => t.LanguageCode);
    }
}
