using FinanceTracker.App.Accounts.Domain.Entities;

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

/// <summary>
/// Методы расширения для преобразования DTO создания типа счёта в доменную модель.
/// </summary>
public static class CreateAccountTypeDtoExtensions
{
    /// <summary>
    /// Преобразует DTO создания типа счёта в доменную модель.
    /// </summary>
    /// <param name="dto">
    /// DTO с данными для создания типа счёта.
    /// </param>
    /// <returns>
    /// Новая доменная модель типа счёта.
    /// </returns>
    public static AccountType ToModel(this CreateAccountTypeDto dto)
    {
        return new AccountType
        {
            Id = Guid.NewGuid(),
            Code = dto.Code.Trim(),
            Description = dto.Description.Trim(),
            IsArchived = false,
        };
    }
}
