using System.Linq.Expressions;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class LastOrderedResultsSpecification() : ISpecification<ResultEntity>
{
    public Expression<Func<ResultEntity, bool>>? Criteria { get; } = null;

    public Expression<Func<ResultEntity, object>> OrderBy { get; } = r => r.FileName; 

    public Expression<Func<ResultEntity, object>>? ThenBy { get; } = r => r.MinDate;

    public bool? OrderByDescending { get; } = true;

    public bool? ThenByDescending { get; } = true;

    public int? Take { get; } = 10;

}
