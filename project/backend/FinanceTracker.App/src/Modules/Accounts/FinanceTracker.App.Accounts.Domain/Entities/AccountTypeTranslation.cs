using FinanceTracker.App.ShareKernel.Domain.Localization;

namespace FinanceTracker.App.Accounts.Domain.Entities;

/// <summary>
/// Перевод типа счёта на различные языки.
/// </summary>
public class AccountTypeTranslation : TranslationBase<Guid>
{
    /// <summary>
    /// Описание типа счёта на соответствующем языке.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Навигационное свойство к родительской сущности.
    /// </summary>
    public AccountType AccountType { get; set; } = null!;
}