using FinanceTracker.App.ShareKernel.Application.Localization;

namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

/// <summary>
/// DTO для перевода типа счёта на определённый язык.
/// </summary>
public sealed record AccountTypeTranslationDto : TranslationDto
{
    /// <summary>
    /// Описание типа счёта на соответствующем языке.
    /// </summary>
    public required string Description { get; init; }
}
