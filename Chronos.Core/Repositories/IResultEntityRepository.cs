using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Core.Repositories;

public interface IResultEntityRepository : IRepository<ResultEntity>
{
    Task<IReadOnlyList<ResultEntity>> GetFilteredAsync(ISpecification<ResultEntity> specification,
                                                  CancellationToken token = default);

    Task DeleteByFileNameAsync(string fileName, CancellationToken token);
}
