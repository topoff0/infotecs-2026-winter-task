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

            return BadRequest(result.Error!.Description);
        }


        [HttpGet("results-filtered")]
        public async Task<IActionResult> GetResultsWithFilters([FromQuery] ResultFilters filters, CancellationToken token)
        {
            var query = new GetResultsWithFiltersQuery(filters);

            var result = await _mediator.Send(query, token);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error!.Description);
        }

        [HttpGet("results-by-filename-last")]
        public async Task<IActionResult> GetLastResults([FromQuery] string fileName, CancellationToken token)
        {
            // NOTE:In the future can be changed the result's count
            // (just change the request fileName to 'LastResultsByFileNameFilter'
            var filter = new LastResultsByFileNameFilter(fileName);

            var query = new GetLastResultsByFileNameQuery(filter);

            var result = await _mediator.Send(query, token);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error!.Description);
        }
    }
}
