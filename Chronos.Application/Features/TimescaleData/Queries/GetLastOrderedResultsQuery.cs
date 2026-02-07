using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Logger;
using Chronos.Application.Specifications;
using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chronos.Application.Features.TimescaleData.Queries;

public record GetLastOrderedResultsQuery()
    : IRequest<ResultT<IReadOnlyList<ResultEntity>>>;

public sealed class GetLastOrderedResultsQueryHandler(IResultEntityRepository resultEntityRepository,
                                                      ILogger<GetLastOrderedResultsQueryHandler> logger)
    : IRequestHandler<GetLastOrderedResultsQuery, ResultT<IReadOnlyList<ResultEntity>>>
{
    private readonly IResultEntityRepository _resultEntityRepository = resultEntityRepository;
    private readonly ILogger<GetLastOrderedResultsQueryHandler> _logger = logger;

    public async Task<ResultT<IReadOnlyList<ResultEntity>>> Handle(GetLastOrderedResultsQuery request, CancellationToken token)
    {
        try
        {
            _logger.LogStartGetOrderedLastResults();

            var specification = new LastOrderedResultsSpecification();

            var results = await _resultEntityRepository.GetFilteredAsync(specification, token);

            _logger.LogSuccessfulGetLastOrderedResults(results.Count);

            return ResultT<IReadOnlyList<ResultEntity>>.Success(results);
        }
        catch (Exception ex)
        {
            _logger.LogGetLastOrderedResultsUnexpectedError(ex.Message);
            return GetResultsErrors.GetLastResultsByFileNameError(); 
        }
    }
}
