using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Core.Repositories;

public interface IResultRepository : IRepository<Result>
{
    Task<IReadOnlyList<Result>> GetFiilteredAsync(ISpecification<Result> specification,
                                                  CancellationToken token = default);
}
