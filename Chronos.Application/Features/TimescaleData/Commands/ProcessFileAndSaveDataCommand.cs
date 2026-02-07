using System.ComponentModel.DataAnnotations;
using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.DTOs.Requests;
using Chronos.Application.Features.TimescaleData.Errors;
using Chronos.Application.Logger;
using Chronos.Application.Services;
using Chronos.Core.Repositories;
using Chronos.Core.Repositories.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = Chronos.Application.Common.Results.Unit;

namespace Chronos.Application.Features.TimescaleData.Commands;

public record ProcessFileAndSaveDataCommand(string FileName, Stream CsvStream)
    : IRequest<ResultT<Unit>>;


public sealed class ProcessFileAndSaveDataCommandHandler(IValueEntityRepository valueRepository,
                                                         IResultEntityRepository resultRepository,
                                                         IUnitOfWork unitOfWork,
                                                         IResultCalculator resultCalculator,
                                                         ICsvParser csvParser,
                                                         ILogger<ProcessFileAndSaveDataCommandHandler> logger)
    : IRequestHandler<ProcessFileAndSaveDataCommand, ResultT<Unit>>
{
    private readonly IValueEntityRepository _valueRepository = valueRepository;
    private readonly IResultEntityRepository _resultRepository = resultRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IResultCalculator _resultCalculator = resultCalculator;
    private readonly ICsvParser _csvParser = csvParser;
    private readonly ILogger<ProcessFileAndSaveDataCommandHandler> _logger = logger;

    public async Task<ResultT<Unit>> Handle(ProcessFileAndSaveDataCommand request, CancellationToken token)
    {
        try
        {
            _logger.LogStartProcessingCsvFile(request.FileName);
            var values = await _csvParser.Parse(request.FileName, request.CsvStream, token);

            _logger.LogProcessingItems(values.Count, request.FileName);

            var calculateDto = new CalculateResultDto(request.FileName, values);
            var resultEntity = _resultCalculator.Calculate(calculateDto);

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _valueRepository.DeleteByFileNameAsync(request.FileName, token);
                await _resultRepository.DeleteByFileNameAsync(request.FileName, token);

                await _valueRepository.AddRangeAsync(values, token);
                await _resultRepository.AddAsync(resultEntity, token);

                await _unitOfWork.SaveChangesAsync();
            }, token);

            _logger.LogSuccessfulProcessed(request.FileName);

            return ResultT<Unit>.Success(Unit.Value);
        }
        catch (ValidationException ex)
        {
            _logger.LogCsvFileValidationError(request.FileName, ex.Message);
            return CsvParseErrors.Validation(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogCsvFileUnexpectedError(request.FileName, ex.Message);
            return Error.Failure("unexpected.processFileAndSaveData", "Unexpected error in processing data");
        }


    }
}
