using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Logger;
using Chronos.Application.Specifications;
using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chronos.Application.Features.TimescaleData.Queries;

public record GetResultsWithFiltersQuery(ResultFilters Filters)
    : IRequest<ResultT<IReadOnlyList<ResultEntity>>>;

public sealed class GetResultWithFilterQueryHandler(IResultEntityRepository resultRepository,
                                                    ILogger<GetResultWithFilterQueryHandler> logger)
    : IRequestHandler<GetResultsWithFiltersQuery, ResultT<IReadOnlyList<ResultEntity>>>
{
    private readonly IResultEntityRepository _resultRepository = resultRepository;
    private readonly ILogger<GetResultWithFilterQueryHandler> _logger = logger;

    public async Task<ResultT<IReadOnlyList<ResultEntity>>> Handle(GetResultsWithFiltersQuery request, CancellationToken token)
    {
        var validation = request.Filters.Validate();
        if (!validation.IsSuccess)
            return ResultT<IReadOnlyList<ResultEntity>>.Failure(validation.Error!);

        try
        {
            _logger.LogStartGetResultsWithFilters(request.Filters);

            var filterSpecification = new ResultByFilterSpecification(request.Filters);

            var results = await _resultRepository.GetFilteredAsync(filterSpecification, token);

            _logger.LogSuccessfulGetFilteredResults(results.Count, request.Filters);

            return ResultT<IReadOnlyList<ResultEntity>>.Success(results);
        }
        catch (Exception ex)
        {
            _logger.LogGetFilteredResultsUnexpectedError(ex.Message);
            return GetResultsErrors.GetFilteredResultsError();
        }
    }
}

