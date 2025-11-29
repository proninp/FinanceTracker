using FinanceTracker.App.ShareKernel.Domain.Entities;
using FinanceTracker.App.ShareKernel.Domain.Localization;

namespace FinanceTracker.App.Accounts.Domain.Entities;

/// <summary>
/// Тип счёта с поддержкой мультиязычности (например: банковский, кредитный, наличные и т.д.).
/// </summary>
public class AccountType : Entity, IArchivableEntity, ITranslatableEntity<AccountTypeTranslation>
{
    /// <summary>
    /// Короткий код типа счёта (инвариант, не переводится).
    /// Примеры: BANK, CASH, CARD, CREDIT, DEPOSIT
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Описание по умолчанию (на русском языке).
    /// Используется как fallback, если перевод не найден.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Флаг, указывающий, архивирован ли тип счёта.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Коллекция переводов для разных языков.
    /// </summary>
    public ICollection<AccountTypeTranslation> Translations { get; set; } = new List<AccountTypeTranslation>();

    /// <summary>
    /// Получить описание на указанном языке.
    /// Если перевод не найден, возвращается описание по умолчанию.
    /// </summary>
    public string GetDescription(string languageCode)
    {
        var translation = Translations.FirstOrDefault(t => t.LanguageCode == languageCode);
        return translation?.Description ?? Description;
    }

    /// <summary>
    /// Получить описание на указанном языке.
    /// </summary>
    public string GetDescription(Language language) =>
        GetDescription(language.ToCode());
}
