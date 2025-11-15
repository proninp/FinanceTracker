using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Accounts.Domain.Entities;

/// <summary>
/// Тип счёта (например: банковский, кредитный, наличные и т.д.).
/// </summary>
public class AccountType : Entity, IArchivableEntity
{
    /// <summary>
    /// Короткий код типа счёта.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Описание типа счёта.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Флаг, указывающий, архивирован ли тип счёта.
    /// </summary>
    public bool IsArchived { get; set; }
}
