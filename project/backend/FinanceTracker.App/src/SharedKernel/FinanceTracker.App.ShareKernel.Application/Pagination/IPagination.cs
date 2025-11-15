namespace FinanceTracker.App.ShareKernel.Application.Pagination;

/// <summary>
/// Интерфейс, обозначающий наличие настроек пагинации.
/// </summary>
public interface IPagination
{
    /// <summary>
    /// Настройки пагинации (текущая страница, размер страницы и прочие параметры).
    /// </summary>
    PaginationSettings PaginationSettings { get; }
}
