using Chronos.Core.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Chronos.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork(ChronosDbContext context) : IUnitOfWork
{
    private readonly ChronosDbContext _context = context;

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken token = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(token);
            await action();
            await transaction.CommitAsync(token);
        });
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}
