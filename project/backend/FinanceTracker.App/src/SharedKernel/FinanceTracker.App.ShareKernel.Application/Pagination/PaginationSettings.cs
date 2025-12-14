namespace FinanceTracker.App.ShareKernel.Application.Pagination;

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

    public int EffectivePageSize => Math.Min(PageSize, MaxPageSize);

    /// <summary>
    /// Количество элементов, которое нужно пропустить при выборке,
    /// исходя из номера текущей страницы и фактического размера страницы.
    /// </summary>
    public int Skip => (PageNumber - 1) * EffectivePageSize;
}
