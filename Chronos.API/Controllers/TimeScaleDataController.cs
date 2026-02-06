using Chronos.API.Contracts.Requests;
using Chronos.Application.Features.ResultsData.Commands;
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
    }
}
