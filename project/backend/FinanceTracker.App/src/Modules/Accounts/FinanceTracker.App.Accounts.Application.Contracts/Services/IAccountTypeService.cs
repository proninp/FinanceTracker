using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

/// <summary>
/// Сервис управления типами счётов.
/// </summary>
public interface IAccountTypeService
{
    /// <summary>
    /// Возвращает тип счёта по идентификатору.
    /// </summary>
    Task<Result<AccountTypeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает тип счёта по коду.
    /// </summary>
    Task<Result<AccountTypeDto>> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает постраничный список типов счётов.
    /// </summary>
    Task<Result<PaginationResult<AccountTypeDto>>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список всех типов счётов.
    /// </summary>
    Task<Result<IReadOnlyList<AccountTypeDto>>> GetAllAsync(
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новый тип счёта.
    /// </summary>
    Task<Result<AccountTypeDto>> CreateAsync(CreateAccountTypeDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующий тип счёта.
    /// </summary>
    Task<Result<AccountTypeDto>> UpdateAsync(UpdateAccountTypeDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет тип счёта.
    /// </summary>
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Архивирует тип счёта.
    /// </summary>
    Task<Result> ArchiveAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Снимает тип счёта с архива.
    /// </summary>
    Task<Result> UnarchiveAsync(Guid id, CancellationToken cancellationToken = default);
}
