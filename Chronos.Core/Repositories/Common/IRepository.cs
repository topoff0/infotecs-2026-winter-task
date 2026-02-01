namespace Chronos.Core.Repositories.Common;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
    Task AddAsync(T entity, CancellationToken token = default);
    void Update(T entity);
    void Delete(T entity);
}
