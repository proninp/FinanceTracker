namespace FinanceTracker.App.ShareKernel.Domain.Entities;

public interface IArchivableEntity
{
    bool IsArchived { get; set; }
}
