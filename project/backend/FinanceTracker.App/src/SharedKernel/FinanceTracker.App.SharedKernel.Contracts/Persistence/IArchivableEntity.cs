namespace FinanceTracker.App.SharedKernel.Contracts.Persistence;

public interface IArchivableEntity
{
    bool IsArchived { get; set; }
}
