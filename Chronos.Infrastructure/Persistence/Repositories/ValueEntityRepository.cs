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

    public async Task AddRangeAsync(IReadOnlyList<ValueEntity> entities, CancellationToken token)
    {
        await _context.AddRangeAsync(entities, token);
    }

    public void Delete(ValueEntity entity)
    {
        _context.ValueEntities.Remove(entity);
    }

    public async Task DeleteByFileNameAsync(string fileName, CancellationToken token)
    {
        var entities = await _context.ValueEntities.Where(v => v.FileName == fileName).ToListAsync(token);

        if (entities is not null)
            _context.RemoveRange(entities);
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
