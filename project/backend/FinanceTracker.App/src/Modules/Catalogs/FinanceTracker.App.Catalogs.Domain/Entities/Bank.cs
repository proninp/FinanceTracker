using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

public sealed class Bank : CreatableEntity
{
    public required Guid CountryId { get; set; }

    public required Country Country { get; set; }

    public required string Name { get; set; }
}
