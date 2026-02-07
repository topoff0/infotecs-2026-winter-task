using System.Linq.Expressions;

namespace Chronos.Core.Repositories.Common;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? ThenBy { get; }
    bool? OrderByDescending { get; }
    bool? ThenByDescending { get; }
    int? Take { get; }
}
