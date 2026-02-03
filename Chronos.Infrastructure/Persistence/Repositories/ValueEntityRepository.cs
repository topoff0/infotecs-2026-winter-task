using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence.Repositories;

public class ValueEntityRepository(ChronosDbContext context) : IValueEntityRepository
{
    private readonly ChronosDbContext _context = context;

    public async Task AddAsync(ValueEntity entity, CancellationToken token = default)
    {
        await _context.ValueEntities.AddAsync(entity, token);
    }

    public void Delete(ValueEntity entity)
    {
        _context.ValueEntities.Remove(entity);
    }

    public async Task<IEnumerable<ValueEntity>> GetAllAsync(CancellationToken token = default)
    {
        return await _context.ValueEntities.ToListAsync(token);
    }

    public async Task<ValueEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _context.ValueEntities.FindAsync([id], token);
    }

    public void Update(ValueEntity entity)
    {
        _context.ValueEntities.Update(entity);
    }
}
