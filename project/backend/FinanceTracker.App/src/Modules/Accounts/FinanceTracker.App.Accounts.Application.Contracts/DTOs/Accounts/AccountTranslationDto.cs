namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

public sealed record AccountTranslationDto
{
    public required string LanguageCode { get; init; }

    public required string Name { get; init; }
}
