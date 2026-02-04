namespace Chronos.Core.Repositories.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token = default);

    Task BeginAsync(CancellationToken token = default);
    Task CommitAsync(CancellationToken token = default);
    Task RollBackAsync();
}
