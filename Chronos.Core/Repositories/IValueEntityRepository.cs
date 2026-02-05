using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Core.Repositories;

public interface IValueEntityRepository: IRepository<ValueEntity>
{
    Task DeleteByFileNameAsync(string fileName, CancellationToken token);

    Task AddRangeAsync(IReadOnlyList<ValueEntity> entities, CancellationToken token);
}
