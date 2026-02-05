using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Core.Repositories;

public interface IResultEntityRepository : IRepository<ResultEntity>
{
    Task<IReadOnlyList<ResultEntity>> GetFiilteredAsync(ISpecification<ResultEntity> specification,
                                                  CancellationToken token = default);

    Task DeleteByFileName(string fileName, CancellationToken token);
}
