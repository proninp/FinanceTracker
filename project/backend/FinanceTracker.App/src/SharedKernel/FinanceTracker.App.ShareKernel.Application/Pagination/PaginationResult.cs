namespace FinanceTracker.App.ShareKernel.Application.Pagination;

/// <summary>
/// Результат постраничной выборки с данными и информацией о пагинации.
/// </summary>
/// <typeparam name="T">Тип элементов в наборе данных.</typeparam>
public sealed record PaginationResult<T>
{
    private PaginationResult() { }

    public PaginationResult(List<T> data, PaginationSettings settings, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(settings);

        var effectivePageSize = settings.EffectivePageSize;
        if (effectivePageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(settings));

        Data = data;
        PageNumber = settings.PageNumber;
        PageSize = effectivePageSize;
        TotalPages = totalCount > 0
            ? (int)Math.Ceiling(totalCount / (double)effectivePageSize)
            : 0;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Элементы текущей страницы.
    /// </summary>
    public IReadOnlyList<T> Data { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Номер текущей страницы (0- или 1-индексация по договорённости).
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Общее количество элементов во всём наборе.
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// Пустой результат пагинации с нулевыми параметрами.
    /// </summary>
    public static PaginationResult<T> Empty() =>
        Empty(0, 0);

    /// <summary>
    /// Пустой результат пагинации с указанными номером страницы и размером.
    /// </summary>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    public static PaginationResult<T> Empty(int pageNumber, int pageSize) =>
        new()
        {
            Data = [],
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = 0,
            TotalCount = 0
        };
}
