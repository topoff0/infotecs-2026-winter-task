namespace Chronos.Core.Repositories.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token = default);

    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken token = default);
}
