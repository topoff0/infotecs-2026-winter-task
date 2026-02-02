using Chronos.Core.Repositories.Common;

namespace Chronos.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork(ChronosDbContext context) : IUnitOfWork
{
    private readonly ChronosDbContext _context = context;
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}
