using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Specifications;
using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using MediatR;

namespace Chronos.Application.Features.TimescaleData.Queries;

public record GetLastOrderedResultsQuery()
    : IRequest<ResultT<IReadOnlyList<ResultEntity>>>;

public sealed class GetLastOrderedResultsQueryHandler(IResultEntityRepository resultEntityRepository)
    : IRequestHandler<GetLastOrderedResultsQuery, ResultT<IReadOnlyList<ResultEntity>>>
{
    private readonly IResultEntityRepository _resultEntityRepository = resultEntityRepository;

    public async Task<ResultT<IReadOnlyList<ResultEntity>>> Handle(GetLastOrderedResultsQuery request, CancellationToken token)
    {
        try
        {
            var specification = new LastOrderedResultsSpecification();

            var results = await _resultEntityRepository.GetFilteredAsync(specification, token);

            return ResultT<IReadOnlyList<ResultEntity>>.Success(results);
        }
        catch (Exception ex)
        {
            return GetResultsErrors.GetLastResultsByFileNameError(); 
        }
    }
}
