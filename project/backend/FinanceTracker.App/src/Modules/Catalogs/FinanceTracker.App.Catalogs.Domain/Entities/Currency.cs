using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

/// <summary>
/// Информация о валюте.
/// </summary>
public sealed class Currency : CreatableEntity
{
    /// <summary>
    /// Наименование валюты.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Трёхбуквенный буквенный код валюты (ISO 4217).
    /// </summary>
    public required string CharCode { get; set; }

    /// <summary>
    /// Трёхзначный числовой код валюты (ISO 4217).
    /// </summary>
    public required int NumCode { get; set; }

    /// <summary>
    /// Символ валюты (например $, €, ₽).
    /// </summary>
    public string? Sign { get; set; }

    /// <summary>
    /// Эмодзи валюты при наличии.
    /// </summary>
    public string? Emoji { get; set; }
}
