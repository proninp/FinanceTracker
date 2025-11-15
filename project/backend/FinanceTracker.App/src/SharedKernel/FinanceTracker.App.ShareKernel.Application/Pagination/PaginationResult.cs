namespace FinanceTracker.App.ShareKernel.Application.Pagination;

/// <summary>
/// Результат постраничной выборки с данными и информацией о пагинации.
/// </summary>
/// <typeparam name="T">Тип элементов в наборе данных.</typeparam>
public sealed record PaginationResult<T>
{
    /// <summary>
    /// Элементы текущей страницы.
    /// </summary>
    public required List<T> Data { get; init; }

    /// <summary>
    /// Номер текущей страницы (0- или 1-индексация по договорённости).
    /// </summary>
    public required int PageNumber { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    public required int PageSize { get; init; }

    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public required int TotalPages { get; init; }

    /// <summary>
    /// Общее количество элементов во всём наборе.
    /// </summary>
    public required int TotalCount { get; init; }

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
            TotalCount = 0,
        };
}
