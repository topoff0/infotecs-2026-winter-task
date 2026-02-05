using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using Chronos.Core.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence.Repositories;

public class ResultEntityRepository(ChronosDbContext context) : IResultEntityRepository
{
    private readonly ChronosDbContext _context = context;

    public async Task AddAsync(ResultEntity entity, CancellationToken token = default)
    {
        await _context.ResultEntities.AddAsync(entity, token);
    }

    public void Delete(ResultEntity entity)
    {
        _context.ResultEntities.Remove(entity);
    }

    public async Task DeleteByFileName(string fileName, CancellationToken token)
    {
        var entity = await _context.ResultEntities.FirstOrDefaultAsync(v => v.FileName == fileName, token);

        if (entity is not null)
            _context.Remove(entity);
    }

    public async Task<IEnumerable<ResultEntity>> GetAllAsync(CancellationToken token = default)
    {
        return await _context.ResultEntities.ToListAsync(token);
    }

    public async Task<ResultEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _context.ResultEntities.FindAsync([id], token);
    }

    public async Task<IReadOnlyList<ResultEntity>> GetFiilteredAsync(ISpecification<ResultEntity> specification, CancellationToken token = default)
    {
        IQueryable<ResultEntity> query = _context.ResultEntities;

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

    public void Update(ResultEntity entity)
    {
        _context.ResultEntities.Update(entity);
    }
}
