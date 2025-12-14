namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

/// <summary>
/// DTO для перевода типа счёта на определённый язык.
/// </summary>
public sealed record AccountTypeTranslationDto
{
    /// <summary>
    /// Код языка (ISO 639-1), например: "ru", "en".
    /// </summary>
    public required string LanguageCode { get; init; }

    /// <summary>
    /// Описание типа счёта на соответствующем языке.
    /// </summary>
    public required string Description { get; init; }
}
