using FinanceTracker.App.ShareKernel.Domain.Localization;

namespace FinanceTracker.App.Accounts.Domain.Entities;

public class AccountTranslation : TranslationBase<Guid>
{
    /// <summary>
    /// Название счёта на соответствующем языке.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Навигационное свойство к родительской сущности.
    /// </summary>
    public Account Account { get; set; } = null!;
}
