using System.Linq.Expressions;

namespace Chronos.Core.Repositories.Common;

public interface ISpecification<T> 
{
    Expression<Func<T, bool>> Criteria { get; }
}
