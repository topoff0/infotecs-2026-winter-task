using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Specifications;
using Chronos.Core.Entities;
using Chronos.Core.Repositories;
using MediatR;

namespace Chronos.Application.Features.TimescaleData.Queries;

public record GetLastResultsByFileNameQuery(LastResultsByFileNameFilter Filter)
    : IRequest<ResultT<IReadOnlyList<ResultEntity>>>;

public sealed class GetLastResultsByFileNameQueryHandler(IResultEntityRepository resultEntityRepository)
    : IRequestHandler<GetLastResultsByFileNameQuery, ResultT<IReadOnlyList<ResultEntity>>>
{
    private readonly IResultEntityRepository _resultEntityRepository = resultEntityRepository;

    public async Task<ResultT<IReadOnlyList<ResultEntity>>> Handle(GetLastResultsByFileNameQuery request, CancellationToken token)
    {
        try
        {
            var specification = new LastResultsByFileSpecification(request.Filter);

            var results = await _resultEntityRepository.GetFilteredAsync(specification, token);

            return ResultT<IReadOnlyList<ResultEntity>>.Success(results);
        }
        catch (Exception ex)
        {
            return GetResultsErrors.GetLastResultsByFileNameError(); 
        }
    }
}
