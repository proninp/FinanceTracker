using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Transactions.Domain.Entities;

/// <summary>
/// Категория финансовых операций.
/// </summary>
public sealed class Category : SoftDeletableEntity
{
    /// <summary>
    /// Идентификатор пользователя, которому принадлежит категория.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Название категории.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Признак категории доходов.
    /// </summary>
    public bool Income { get; set; }

    /// <summary>
    /// Признак категории расходов. По умолчанию — true.
    /// </summary>
    public bool Expense { get; set; } = true;

    /// <summary>
    /// Эмодзи для отображения категории.
    /// </summary>
    public string? Emoji { get; set; }

    /// <summary>
    /// Иконка категории в виде бинарных данных.
    /// </summary>
    public byte[]? Icon { get; set; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Родительская категория.
    /// </summary>
    public Category? ParentCategory { get; set; }

    /// <summary>
    /// Дочерние категории.
    /// </summary>
    public List<Category> Children { get; set; } = [];
}
