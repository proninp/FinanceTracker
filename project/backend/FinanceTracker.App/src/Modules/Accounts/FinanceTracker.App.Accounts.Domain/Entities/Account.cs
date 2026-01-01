using FinanceTracker.App.ShareKernel.Domain.Entities;
using FinanceTracker.App.ShareKernel.Domain.Localization;

namespace FinanceTracker.App.Accounts.Domain.Entities;

/// <summary>
/// Сущность банковского/платёжного счёта пользователя.
/// </summary>
public class Account : Entity, IArchivableEntity, ITranslatableEntity<AccountTranslation>
{
    /// <summary>
    /// Идентификатор пользователя, которому принадлежит счёт.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор типа счёта (ссылка на AccountType).
    /// </summary>
    public required Guid AccountTypeId { get; set; }

    /// <summary>
    /// Тип счёта.
    /// </summary>
    public AccountType AccountType { get; set; } = null!;

    /// <summary>
    /// Идентификатор валюты счёта.
    /// </summary>
    public required Guid CurrencyId { get; set; }

    /// <summary>
    /// Внешний или связанный идентификатор (опционально).
    /// </summary>
    public Guid? BankId { get; set; }

    /// <summary>
    /// Название счёта.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Кредитный лимит по счёту (может быть null).
    /// </summary>
    public decimal? CreditLimit { get; set; }

    /// <summary>
    /// Участвует ли счёт в расчёте общего баланса.
    /// </summary>
    public bool IsIncludeInBalance { get; set; } = false;

    /// <summary>
    /// Пометка, что этот счёт является счётом по умолчанию для пользователя.
    /// </summary>
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Флаг, указывающий, архивирован ли счёт.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Коллекция переводов для разных языков.
    /// </summary>
    public ICollection<AccountTranslation> Translations { get; set; } = new List<AccountTranslation>();

    /// <summary>
    /// Получить описание на указанном языке.
    /// Если перевод не найден, возвращается описание по умолчанию.
    /// </summary>
    public string GetName(string languageCode)
    {
        var translation = Translations.FirstOrDefault(t => t.LanguageCode == languageCode);
        return translation?.Name ?? Name;
    }

    /// <summary>
    /// Получить описание на указанном языке.
    /// </summary>
    public string GetName(Language language) =>
        GetName(language.ToCode());
}
