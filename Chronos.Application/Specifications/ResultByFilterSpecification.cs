using System.Linq.Expressions;
using Chronos.Application.Features.ResultsData.DTOs;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class ResultByFilterSpecification(ResultFilter filter) : ISpecification<Result>
{
    public Expression<Func<Result, bool>> Criteria { get; private set; } = r =>
            (filter.FileName == null || r.FileName == filter.FileName) &&
            (!filter.FirstOperationsStartedFrom.HasValue || r.MinDate >= filter.FirstOperationsStartedFrom) &&
            (!filter.FirstOperationStartedTo.HasValue || r.MinDate <= filter.FirstOperationStartedTo) &&
            (!filter.AvgNumericValueFrom.HasValue || r.AvgNumericValue >= filter.AvgNumericValueFrom) &&
            (!filter.AvgNumericValueTo.HasValue || r.AvgNumericValue <= filter.AvgNumericValueTo) &&
            (!filter.AvgExecutionTimeFrom.HasValue || r.AvgExecutionTime >= filter.AvgExecutionTimeFrom) &&
            (!filter.AvgExecutionTimeTo.HasValue || r.AvgExecutionTime <= filter.AvgExecutionTimeTo);
}
