using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Repositories;

/// <summary>
/// Репозиторий для работы с типами счетов (AccountType).
/// </summary>
public interface IAccountTypeRepository
{
    /// <summary>
    /// Получить тип счёта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор типа счёта.</param>
    /// <param name="languageCode">Код языка для фильтрации переводов. Если не указан, возвращаются все.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность AccountType или null, если не найдена.</returns>
    Task<AccountType?> GetByIdAsync(Guid id, string? languageCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить тип счёта по коду.
    /// </summary>
    /// <param name="code">Уникальный код типа счёта.</param>
    /// <param name="languageCode">Код языка для фильтрации переводов. Если не указан, возвращаются все.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность AccountType или null, если не найдена.</returns>
    Task<AccountType?> GetByCodeAsync(string code, string? languageCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить постраничный список типов счётов.
    /// </summary>
    /// <param name="settings">Настройки пагинации.</param>
    /// <param name="languageCode">Код языка для фильтрации переводов. Если не указан, возвращаются все.</param>
    /// <param name="includeArchived">Включать ли архивные записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат пагинации со списком AccountType.</returns>
    Task<PaginationResult<AccountType>> GetPagedAsync(PaginationSettings settings,
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Получить все типы счётов.
    /// </summary>
    /// <param name="languageCode">Код языка для фильтрации переводов. Если не указан, возвращаются все.</param>
    /// <param name="includeArchived">Включать ли архивные записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех AccountType.</returns>
    Task<IReadOnlyList<AccountType>> GetAllAsync(string? languageCode = null, bool includeArchived = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Проверить существование типа счёта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор типа счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True если существует, иначе false.</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить существование типа счёта по коду.
    /// </summary>
    /// <param name="code">Код типа счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True если код уже существует, иначе false.</returns>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новый тип счёта.
    /// </summary>
    /// <param name="accountType">Сущность AccountType для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленная сущность AccountType.</returns>
    Task<AccountType> AddAsync(AccountType accountType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующий тип счёта.
    /// </summary>
    /// <param name="accountType">Сущность AccountType с изменениями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateAsync(AccountType accountType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить тип счёта.
    /// </summary>
    /// <param name="accountType">Сущность AccountType для удаления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteAsync(AccountType accountType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все типы счётов, включая удалённые при необходимости.
    /// </summary>
    /// <param name="languageCode">Код языка для фильтрации переводов. Если не указан, возвращаются все.</param>s
    /// <param name="includeArchived">Включать ли архивные и/или удалённые записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сущностей AccountType.</returns>
    Task<IReadOnlyList<AccountType>> GetAllIncludingDeletedAsync(
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    );
}
