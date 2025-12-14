namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

public sealed record CreateAccountTypeDto
{
    /// <summary>
    /// Короткий код типа счёта (инвариант).
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Описание по умолчанию (fallback, обычно на русском).
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Переводы на другие языки (опционально).
    /// </summary>
    public ICollection<AccountTypeTranslationDto>? Translations { get; init; }
}
