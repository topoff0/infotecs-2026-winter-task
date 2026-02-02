using System.Linq.Expressions;
using Chronos.Application.Features.ResultsData.DTOs;
using Chronos.Core.Entities;
using Chronos.Core.Repositories.Common;

namespace Chronos.Application.Specifications;

public sealed class LastResultsByFileSpecification(LastResultsByFileFilter filter) : ISpecification<Result>
{
    public Expression<Func<Result, bool>> Criteria { get; } = r => r.FileName == filter.FileName;
    //NOTE: Maybe do it better *_*
    public Expression<Func<Result, object>> OrderBy { get; } = r => r.MinDate; 

    public bool? OrderByDescending { get; } = true;

    public int? Take { get; } = 10;
}
