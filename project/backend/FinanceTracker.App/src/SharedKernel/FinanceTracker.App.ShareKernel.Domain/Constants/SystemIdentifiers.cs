namespace FinanceTracker.App.ShareKernel.Domain.Constants;

/// <summary>
/// Системные идентификаторы, используемые для операций, выполняемых системой
/// (миграции, сидинг данных, фоновые задачи и т.д.).
/// </summary>
public static class SystemIdentifiers
{
    /// <summary>
    /// Идентификатор системного пользователя.
    /// Используется для операций, которые выполняются системой автоматически,
    /// а не от имени конкретного пользователя.
    /// </summary>
    public static readonly Guid SystemUserId = new("00000000-0000-0000-0000-000000000001");

    /// <summary>
    /// Идентификатор типа счета - банковский счёт.
    /// </summary>
    public static readonly Guid BankAccountTypeId = new("10000000-0000-0000-0000-000000000001");

    /// <summary>
    /// Идентификатор типа счета - наличные.
    /// </summary>
    public static readonly Guid CashAccountTypeId = new("10000000-0000-0000-0000-000000000002");

    /// <summary>
    /// Идентификатор типа счета - банковская карта.
    /// </summary>
    public static readonly Guid CardAccountTypeId = new("10000000-0000-0000-0000-000000000003");

    /// <summary>
    /// Идентификатор типа счета - кредитный счёт.
    /// </summary>
    public static readonly Guid CreditAccountTypeId = new("10000000-0000-0000-0000-000000000004");

    /// <summary>
    /// Идентификатор типа счета - депозитный счёт.
    /// </summary>
    public static readonly Guid DepositAccountTypeId = new("10000000-0000-0000-0000-000000000005");
}
