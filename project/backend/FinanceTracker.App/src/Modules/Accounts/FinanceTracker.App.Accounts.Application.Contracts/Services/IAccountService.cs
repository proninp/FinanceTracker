using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

/// <summary>
/// Сервис управления счетами пользователя.
/// Предоставляет операции получения, создания, обновления
/// и управления жизненным циклом счетов.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Возвращает счёт по идентификатору.
    /// </summary>
    Task<Result<AccountDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает счёт по идентификатору с проверкой принадлежности пользователю.
    /// </summary>
    Task<Result<AccountDto?>> GetByIdForUserAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает постраничный список счетов пользователя.
    /// </summary>
    Task<Result<PaginationResult<AccountDto>>> GetPagedAsync(
        PaginationSettings settings,
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает весь список счетов пользователя.
    /// </summary>
    Task<Result<IReadOnlyList<AccountDto>>> GetUserAccountsAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает счёт пользователя, установленный по умолчанию.
    /// </summary>
    Task<Result<AccountDto?>> GetDefaultAccountForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новый счёт.
    /// </summary>
    Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет данные счёта.
    /// </summary>
    Task<Result<AccountDto>> UpdateAsync(UpdateAccountDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет счёт пользователя.
    /// </summary>
    Task<Result> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Архивирует счёт пользователя.
    /// </summary>
    Task<Result> ArchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Снимает счёт пользователя с архива.
    /// </summary>
    Task<Result> UnarchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Назначает счёт пользователю как счёт по умолчанию.
    /// </summary>
    Task<Result> SetAsDefaultAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
