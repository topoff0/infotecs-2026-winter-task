using System.Linq.Expressions;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class ResultByFilterSpecification(ResultFilters filters) : ISpecification<ResultEntity>
{
    public Expression<Func<ResultEntity, bool>> Criteria { get; } = r =>
            (filters.FileName == null || r.FileName == filters.FileName) &&
            (!filters.FirstOperationsStartedFrom.HasValue || r.MinDate >= filters.FirstOperationsStartedFrom) &&
            (!filters.FirstOperationStartedTo.HasValue || r.MinDate <= filters.FirstOperationStartedTo) &&
            (!filters.AvgNumericValueFrom.HasValue || r.AvgNumericValue >= filters.AvgNumericValueFrom) &&
            (!filters.AvgNumericValueTo.HasValue || r.AvgNumericValue <= filters.AvgNumericValueTo) &&
            (!filters.AvgExecutionTimeFrom.HasValue || r.AvgExecutionTime >= filters.AvgExecutionTimeFrom) &&
            (!filters.AvgExecutionTimeTo.HasValue || r.AvgExecutionTime <= filters.AvgExecutionTimeTo);

    public Expression<Func<ResultEntity, object>>? OrderBy { get; } = null; 

    public bool? OrderByDescending { get; } = null; 

    public int? Take { get; } = null;

    public Expression<Func<ResultEntity, object>>? ThenBy { get; } = null;

    public bool? ThenByDescending { get; } = null;
}
