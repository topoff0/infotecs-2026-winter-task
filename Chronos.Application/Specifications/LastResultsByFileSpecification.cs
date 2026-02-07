using System.Linq.Expressions;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class LastResultsByFileSpecification(LastResultsByFileNameFilter filter) : ISpecification<ResultEntity>
{
    public Expression<Func<ResultEntity, bool>> Criteria { get; } = r => r.FileName == filter.FileName;
    //NOTE: Maybe do it better *_*
    public Expression<Func<ResultEntity, object>> OrderBy { get; } = r => r.MinDate; 

    public bool? OrderByDescending { get; } = true;

    public int? Take { get; } = 10;
}
