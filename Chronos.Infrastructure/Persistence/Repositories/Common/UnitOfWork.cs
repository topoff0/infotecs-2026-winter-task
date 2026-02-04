using Chronos.Core.Repositories.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Chronos.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork(ChronosDbContext context) : IUnitOfWork
{
    private readonly ChronosDbContext _context = context;
    private IDbContextTransaction? _transaction;

    public async Task BeginAsync(CancellationToken token = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(token);
    }

    public async Task CommitAsync(CancellationToken token = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        await _context.SaveChangesAsync(token);
        await _transaction.CommitAsync(token);
    }

    public async Task RollBackAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        await _transaction.RollbackAsync(CancellationToken.None);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}
