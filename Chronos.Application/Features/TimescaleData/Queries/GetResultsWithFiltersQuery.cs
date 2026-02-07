using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Specifications;
using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using MediatR;

namespace Chronos.Application.Features.TimescaleData.Queries;

public record GetResultsWithFiltersQuery(ResultFilters Filters)
    : IRequest<ResultT<IReadOnlyList<ResultEntity>>>;

public sealed class GetResultWithFilterQueryHandler(IResultEntityRepository resultRepository)
    : IRequestHandler<GetResultsWithFiltersQuery, ResultT<IReadOnlyList<ResultEntity>>>
{
    private readonly IResultEntityRepository _resultRepository = resultRepository;

    public async Task<ResultT<IReadOnlyList<ResultEntity>>> Handle(GetResultsWithFiltersQuery request, CancellationToken token)
    {
        try
        {
            var filterSpecification = new ResultByFilterSpecification(request.Filters);

            var results = await _resultRepository.GetFilteredAsync(filterSpecification, token);

            return ResultT<IReadOnlyList<ResultEntity>>.Success(results);
        }
        catch (Exception ex)
        {
            return GetResultsErrors.GetFilteredResultsError();
        }
    }
}
