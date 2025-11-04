namespace FinanceTracker.App.SharedKernel.Contracts.Pagination;

/// <summary>
/// Настройки пагинации.
/// </summary>
/// <param name="PageNumber">Номер запрашиваемой страницы (обычно начинается с 1).</param>
/// <param name="PageSize">Количество элементов на одной странице.</param>
public sealed record PaginationSettings(int PageNumber, int PageSize)
{
    /// <summary>
    /// Максимально допустимый размер страницы. Значение по умолчанию — 100.
    /// </summary>
    public int MaxPageSize { get; init; } = 100;
}
