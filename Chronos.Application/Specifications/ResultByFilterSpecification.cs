using System.Linq.Expressions;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class ResultByFilterSpecification(ResultFilter filter) : ISpecification<ResultEntity>
{
    public Expression<Func<ResultEntity, bool>> Criteria { get; } = r =>
            (filter.FileName == null || r.FileName == filter.FileName) &&
            (!filter.FirstOperationsStartedFrom.HasValue || r.MinDate >= filter.FirstOperationsStartedFrom) &&
            (!filter.FirstOperationStartedTo.HasValue || r.MinDate <= filter.FirstOperationStartedTo) &&
            (!filter.AvgNumericValueFrom.HasValue || r.AvgNumericValue >= filter.AvgNumericValueFrom) &&
            (!filter.AvgNumericValueTo.HasValue || r.AvgNumericValue <= filter.AvgNumericValueTo) &&
            (!filter.AvgExecutionTimeFrom.HasValue || r.AvgExecutionTime >= filter.AvgExecutionTimeFrom) &&
            (!filter.AvgExecutionTimeTo.HasValue || r.AvgExecutionTime <= filter.AvgExecutionTimeTo);

    public Expression<Func<ResultEntity, object>>? OrderBy { get; } = null; 

    public bool? OrderByDescending { get; } = null; 

    public int? Take { get; } = null;
}
