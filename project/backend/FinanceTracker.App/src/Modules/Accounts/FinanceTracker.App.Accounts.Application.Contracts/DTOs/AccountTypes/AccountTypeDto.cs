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

/// <summary>
/// Методы расширения для преобразования типа счёта в DTO.
/// </summary>
public static class AccountTypeDtoExtensions
{
    /// <summary>
    /// Преобразует доменную модель типа счёта в DTO с учётом языка.
    /// </summary>
    /// <param name="accountType">
    /// Доменная модель типа счёта.
    /// </param>
    /// <param name="languageCode">
    /// Код языка (ISO 639-1), используемый для получения локализованного описания.
    /// </param>
    /// <returns>
    /// DTO типа счёта с локализованным описанием.
    /// </returns>
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
