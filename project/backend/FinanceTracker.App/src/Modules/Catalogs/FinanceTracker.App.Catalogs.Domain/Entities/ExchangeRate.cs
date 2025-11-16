using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

/// <summary>
/// Курс валюты на определённую дату.
/// </summary>
public sealed class ExchangeRate : CreatableEntity
{
    /// <summary>
    /// Дата, к которой относится курс.
    /// </summary>
    public required DateTime RateDate { get; set; }

    /// <summary>
    /// Идентификатор валюты.
    /// </summary>
    public required Guid CurrencyId { get; set; }

    /// <summary>
    /// Валюта, для которой указан курс.
    /// </summary>
    public required Currency Currency { get; set; }

    /// <summary>
    /// Значение курса.
    /// </summary>
    public required decimal Rate { get; set; }
}
