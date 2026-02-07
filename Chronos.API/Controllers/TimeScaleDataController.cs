using Chronos.API.Contracts.Requests;
using Chronos.Application.Features.TimescaleData.Commands;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Chronos.Application.Features.TimescaleData.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.API.Controllers
{
    [Route("api/timescale")]
    [ApiController]
    public sealed class TimescaleDataController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("process-csv-file")]
        public async Task<IActionResult> ProcessFile([FromForm] ProcessFileRequest request, CancellationToken token)
        {
            await using var stream = request.File.OpenReadStream();

            var command = new ProcessFileAndSaveDataCommand(request.File.FileName, stream);
            var result = await _mediator.Send(command, token);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }


        [HttpGet("results-filtered")]
        public async Task<IActionResult> GetResultsWithFilters([FromQuery] ResultFilters filters, CancellationToken token)
        {
            var query = new GetResultsWithFiltersQuery(filters);

            var result = await _mediator.Send(query, token);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpGet("results-by-filename-last")]
        public async Task<IActionResult> GetLastResults(CancellationToken token)
        {
            var query = new GetLastOrderedResultsQuery();

            var result = await _mediator.Send(query, token);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
