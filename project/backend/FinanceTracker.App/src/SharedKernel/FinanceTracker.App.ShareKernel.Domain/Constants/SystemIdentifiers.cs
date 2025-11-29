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
}