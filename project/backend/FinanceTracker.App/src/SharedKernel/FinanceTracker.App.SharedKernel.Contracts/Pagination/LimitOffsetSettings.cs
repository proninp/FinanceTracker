namespace FinanceTracker.App.SharedKernel.Contracts.Pagination;

/// <summary>
/// Настройки выборки с использованием limit/offset: лимит, смещение и ограничение на максимальное количество результатов.
/// </summary>
/// <param name="Limit">Максимальное количество элементов, возвращаемых за один запрос.</param>
/// <param name="Offset">Смещение (количество пропускаемых элементов) от начала набора.</param>
/// <param name="MaxResults">Ограничение по максимальному общему числу результатов (защита от слишком больших запросов).</param>
public sealed record LimitOffsetSettings(int Limit, int Offset, int MaxResults = 100)
{
    /// <summary>
    /// Настройки по умолчанию: Limit = 100, Offset = 0.
    /// </summary>
    public static readonly LimitOffsetSettings Default = new LimitOffsetSettings(100, 0);

    /// <summary>
    /// Максимальное количество элементов для возвращаемой страницы.
    /// </summary>
    public int Limit { get; } = Limit;

    /// <summary>
    /// Смещение от начала набора данных.
    /// </summary>
    public int Offset { get; } = Offset;

    /// <summary>
    /// Максимально допустимое количество результатов всего (защита от больших запросов).
    /// </summary>
    public int MaxResults { get; } = MaxResults;
}
