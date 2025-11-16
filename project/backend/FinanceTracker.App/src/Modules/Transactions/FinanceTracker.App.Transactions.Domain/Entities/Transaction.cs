using FinanceTracker.App.ShareKernel.Domain.Entities;
using FinanceTracker.App.Transactions.Domain.Enums;

namespace FinanceTracker.App.Transactions.Domain.Entities;

/// <summary>
/// Финансовая операция (доход или расход).
/// </summary>
public sealed class Transaction : AuditableEntity
{
    /// <summary>
    /// Идентификатор счёта, к которому относится операция.
    /// </summary>
    public required Guid AccountId { get; set; }

    /// <summary>
    /// Идентификатор категории операции.
    /// </summary>
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// Категория операции.
    /// </summary>
    public required Category Category { get; set; }

    /// <summary>
    /// Тип операции: доход или расход.
    /// </summary>
    public TransacionType Type { get; set; }

    /// <summary>
    /// Сумма операции.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Дата проведения операции.
    /// </summary>
    public required DateTime Date { get; set; }

    /// <summary>
    /// Дополнительное описание операции.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор связанного перевода, если операция является частью перевода.
    /// </summary>
    public Guid? TransferId { get; set; }

    /// <summary>
    /// Информация о переводе, если операция связана с переводом.
    /// </summary>
    public Transfer? Transfer { get; set; }
}
