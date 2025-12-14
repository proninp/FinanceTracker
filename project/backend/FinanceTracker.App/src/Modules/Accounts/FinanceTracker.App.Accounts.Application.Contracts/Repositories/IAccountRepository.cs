using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью счёта (Account).
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Получить счёт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор счёта.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность Account или null, если не найдена.</returns>
    Task<Account?> GetByIdAsync(Guid id, string? languageCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить счёт по идентификатору, принадлежащий указанному пользователю.
    /// </summary>
    /// <param name="id">Идентификатор счёта.</param>
    /// <param name="userId">Идентификатор пользователя-владельца.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность Account или null, если не найдена или не принадлежит пользователю.</returns>
    Task<Account?> GetByIdForUserAsync(Guid id, Guid userId, string? languageCode = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить постраничный список счётов.
    /// </summary>
    /// <param name="settings">Настройки пагинации.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="includeArchived">Включать ли архивные счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат пагинации со списком Account.</returns>
    Task<PaginationResult<Account>> GetPagedAsync(PaginationSettings settings,
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Получить список счётов пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="includeArchived">Включать ли архивные счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сущностей Account.</returns>
    Task<IReadOnlyList<Account>> GetUserAccountsAsync(Guid userId,
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Получить счёт по умолчанию для пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность Account или null, если не задан.</returns>
    Task<Account?> GetDefaultAccountForUserAsync(Guid userId, string? languageCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить существование счёта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True если существует, иначе false.</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить, является ли пользователь владельцем счёта.
    /// </summary>
    /// <param name="accountId">Идентификатор счёта.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True если пользователь является владельцем, иначе false.</returns>
    Task<bool> IsUserAccountOwnerAsync(Guid accountId, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новый счёт.
    /// </summary>
    /// <param name="account">Сущность Account для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленная сущность Account (включая заданный идентификатор и т.д.).</returns>
    Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующий счёт.
    /// </summary>
    /// <param name="account">Сущность Account с изменениями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить счёт.
    /// </summary>
    /// <param name="account">Сущность Account для удаления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteAsync(Account account, CancellationToken cancellationToken = default);

    /// <summary>
    /// Посчитать количество счётов у пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="includeArchived">Включать ли архивные счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество счётов.</returns>
    Task<int> CountUserAccountsAsync(Guid userId, bool includeArchived = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все счёта пользователя, включая удалённые при необходимости.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов связанных сущностей
    /// (например, "en" или "ru"). Если не указан — возвращаются все переводы.
    /// </param>
    /// <param name="includeArchived">Включать ли архивные и/или удалённые счёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сущностей Account.</returns>
    Task<IReadOnlyList<Account>> GetUserAccountsIncludingDeletedAsync(Guid userId,
        string? languageCode,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    );
}
