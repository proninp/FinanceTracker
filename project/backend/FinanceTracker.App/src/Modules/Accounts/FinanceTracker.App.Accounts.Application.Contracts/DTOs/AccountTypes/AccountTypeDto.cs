using FinanceTracker.App.Accounts.Domain.Entities;

namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

public sealed record AccountTypeDto
{
    public required Guid Id { get; init; }

    public required string Code { get; init; }

    /// <summary>
    /// Локализованное описание на текущем языке пользователя.
    /// </summary>
    public required string Description { get; init; }

    public bool IsArchived { get; init; }

    /// <summary>
    /// Все доступные переводы (опционально).
    /// </summary>
    public ICollection<AccountTypeTranslationDto>? Translations { get; init; }
}

public static class AccountTypeDtoExtensions
{
    public static AccountTypeDto ToDto(this AccountType accountType, string languageCode)
    {
        return new AccountTypeDto
        {
            Id = accountType.Id,
            Code = accountType.Code,
            Description = accountType.GetDescription(languageCode),
            IsArchived = accountType.IsArchived
        };
    }
}
