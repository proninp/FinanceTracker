namespace FinanceTracker.App.ShareKernel.Application.Pagination;

/// <summary>
/// Результат пагинации с ограничением и смещением
/// </summary>
/// <typeparam name="T">Тип элементов в результате</typeparam>
/// <param name="Data">Список данных</param>
/// <param name="Limit">Максимальное количество возвращаемых записей</param>
/// <param name="Offset">Смещение относительно первой записи</param>
/// <param name="TotalCount">Общее количество записей</param>
public sealed record LimitOffsetResult<T>(List<T> Data, int Limit, int Offset, int TotalCount)
{
    /// <summary>
    /// Список с данными
    /// </summary>
    public List<T> Data { get; } = Data;

    /// <summary>
    /// Максимальное количество возвращаемых записей
    /// </summary>
    public int Limit { get; } = Limit;

    /// <summary>
    /// Смещение относительно первого элемента
    /// </summary>
    public int Offset { get; } = Offset;

    /// <summary>
    /// Общее количество записей
    /// </summary>
    public int TotalCount { get; } = TotalCount;

    /// <summary>
    /// Возвращает пустой результат
    /// </summary>
    /// <param name="limit">Максимальное количество возвращаемых записей</param>
    /// <param name="offset">Смещение относительно первого элемента</param>
    /// <returns>Пустой результат</returns>
    public static LimitOffsetResult<T> Empty(int limit, int offset) =>
        new([], limit, offset, 0);
}
