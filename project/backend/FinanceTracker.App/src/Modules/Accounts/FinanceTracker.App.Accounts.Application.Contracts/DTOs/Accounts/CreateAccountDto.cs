using FinanceTracker.App.Accounts.Domain.Entities;

namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

/// <summary>
/// DTO для создания нового счёта пользователя.
/// </summary>
public sealed record CreateAccountDto
{
    /// <summary>
    /// Идентификатор пользователя, для которого создаётся счёт.
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Идентификатор типа счёта.
    /// </summary>
    public required Guid AccountTypeId { get; init; }

    /// <summary>
    /// Идентификатор валюты счёта.
    /// </summary>
    public required Guid CurrencyId { get; init; }

    /// <summary>
    /// Идентификатор банка (если применимо).
    /// </summary>
    public Guid? BankId { get; init; }

    /// <summary>
    /// Наименование счёта.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Кредитный лимит по счёту (если применимо).
    /// </summary>
    public decimal? CreditLimit { get; init; }

    /// <summary>
    /// Признак включения счёта в общий баланс.
    /// </summary>
    public bool IsIncludeInBalance { get; init; }

    /// <summary>
    /// Признак установки счёта как счёта по умолчанию.
    /// </summary>
    public bool IsDefault { get; init; }

    /// <summary>
    /// Переводы наименования счёта на другие языки (опционально).
    /// </summary>
    public ICollection<AccountTranslationDto>? Translations { get; init; }
}

/// <summary>
/// Методы расширения для преобразования DTO создания счёта в доменную модель.
/// </summary>
public static class CreateAccountDtoExtensions
{
    /// <summary>
    /// Преобразует DTO создания счёта в доменную модель.
    /// </summary>
    public static Account ToModel(this CreateAccountDto dto)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            AccountTypeId = dto.AccountTypeId,
            CurrencyId = dto.CurrencyId,
            BankId = dto.BankId,
            Name = dto.Name.Trim(),
            CreditLimit = dto.CreditLimit,
            IsIncludeInBalance = dto.IsIncludeInBalance,
            IsDefault = dto.IsDefault,
            IsArchived = false
        };
    }
}
