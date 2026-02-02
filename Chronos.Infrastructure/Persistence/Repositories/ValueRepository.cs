using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence.Repositories;

public class ValueRepository(ChronosDbContext context) : IValueRepository
{
    private readonly ChronosDbContext _context = context;

    public async Task AddAsync(Value entity, CancellationToken token = default)
    {
        await _context.Values.AddAsync(entity, token);
    }

    public void Delete(Value entity)
    {
        _context.Values.Remove(entity);
    }

    public async Task<IEnumerable<Value>> GetAllAsync(CancellationToken token = default)
    {
        return await _context.Values.ToListAsync(token);
    }

    public async Task<Value?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _context.Values.FindAsync([id], token);
    }

    public void Update(Value entity)
    {
        _context.Values.Update(entity);
    }
}
