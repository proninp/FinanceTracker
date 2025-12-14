namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

public sealed record AccountTypeTranslationDto
{
    public required string LanguageCode { get; init; }

    public required string Description { get; init; }
}
