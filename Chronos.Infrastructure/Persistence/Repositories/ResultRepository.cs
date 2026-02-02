using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using Chronos.Core.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence.Repositories;

public class ResultRepository(ChronosDbContext context) : IResultRepository
{
    private readonly ChronosDbContext _context = context;

    public async Task AddAsync(Result entity, CancellationToken token = default)
    {
        await _context.Results.AddAsync(entity, token);
    }

    public void Delete(Result entity)
    {
        _context.Results.Remove(entity);
    }

    public async Task<IEnumerable<Result>> GetAllAsync(CancellationToken token = default)
    {
        return await _context.Results.ToListAsync(token);
    }

    public async Task<Result?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _context.Results.FindAsync([id], token);
    }

    public async Task<IReadOnlyList<Result>> GetFiilteredAsync(ISpecification<Result> specification, CancellationToken token = default)
    {
        IQueryable<Result> query = _context.Results;

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        if (specification.OrderBy != null)
            query = (specification.OrderByDescending != null && specification.OrderByDescending.Value == true)
                ? query.OrderByDescending(specification.OrderBy)
                : query.OrderBy(specification.OrderBy);

        if (specification.Take != null)
            query = query.Take(specification.Take.Value);
        
        return await query.ToListAsync(token);
    }

    public void Update(Result entity)
    {
        _context.Results.Update(entity);
    }
}
