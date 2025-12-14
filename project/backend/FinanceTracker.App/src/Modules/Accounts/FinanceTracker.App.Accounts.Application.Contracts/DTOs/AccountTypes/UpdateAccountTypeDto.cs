namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

public sealed record UpdateAccountTypeDto
{
    public required Guid Id { get; init; }

    public required string Code { get; init; }

    public required string Description { get; init; }

    public bool IsArchived { get; init; }

    /// <summary>
    /// Переводы. Если указаны - заменяют все существующие переводы.
    /// Если null - переводы не изменяются.
    /// </summary>
    public ICollection<AccountTypeTranslationDto>? Translations { get; init; }
}
