using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

public sealed class Currency : CreatableEntity
{
    public required string Name { get; set; }

    public required string CharCode { get; set; }

    public required int NumCode { get; set; }

    public string? Sign { get; set; }

    public string? Emoji { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }
}
