using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Transactions.Domain.Entities;

/// <summary>
/// Перевод средств между двумя счетами.
/// </summary>
public sealed class Transfer : AuditableEntity
{
    /// <summary>
    /// Идентификатор счёта, с которого списываются средства.
    /// </summary>
    public required Guid FromAccountId { get; set; }

    /// <summary>
    /// Идентификатор счёта, на который зачисляются средства.
    /// </summary>
    public required Guid ToAccountId { get; set; }

    /// <summary>
    /// Сумма списания.
    /// </summary>
    public required decimal FromAmount { get; set; }

    /// <summary>
    /// Сумма зачисления.
    /// </summary>
    public required decimal ToAmount { get; set; }

    /// <summary>
    /// Используемый обменный курс. По умолчанию — 1.
    /// </summary>
    public decimal ExchangeRate { get; set; } = 1m;

    /// <summary>
    /// Дополнительное описание перевода.
    /// </summary>
    public string? Description { get; set; }
}
