using System.ComponentModel.DataAnnotations;
using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;
using Chronos.Application.Features.ResultsData.DTOs.Requests;
using Chronos.Application.Features.ResultsData.Errors;
using Chronos.Application.Services;
using Chronos.Core.Repositories;
using Chronos.Core.Repositories.Common;
using MediatR;
using Unit = Chronos.Application.Common.Results.Unit;

namespace Chronos.Application.Features.ResultsData.Commands;

public record ProcessFileAndSaveDataCommand(ProcessFileRequest Dto)
    : IRequest<ResultT<Unit>>;


public sealed class ProcessFileAndSaveDataCommandHandler(IValueEntityRepository valueRepository,
                                                         IResultEntityRepository resultRepository,
                                                         IUnitOfWork unitOfWork,
                                                         IResultCalculator resultCalculator,
                                                         ICsvParser csvParser)
    : IRequestHandler<ProcessFileAndSaveDataCommand, ResultT<Unit>>
{
    private readonly IValueEntityRepository _valueRepository = valueRepository;
    private readonly IResultEntityRepository _resultRepository = resultRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IResultCalculator _resultCalculator = resultCalculator;
    private readonly ICsvParser _csvParser = csvParser;

    public async Task<ResultT<Unit>> Handle(ProcessFileAndSaveDataCommand request, CancellationToken token)
    {
        try
        {
            var values = await _csvParser.Parse(request.Dto.CsvStream, token);


            await _unitOfWork.BeginAsync(token);

            await _valueRepository.DeleteByFileNameAsync(request.Dto.FileName, token);
            await _resultRepository.DeleteByFileName(request.Dto.FileName, token);

            var calculateRequest = new CalculateResultRequest(request.Dto.FileName, values);
            var resultEntity = _resultCalculator.Calculate(calculateRequest);

            await _valueRepository.AddRangeAsync(values, token);
            await _resultRepository.AddAsync(resultEntity, token);

            await _unitOfWork.CommitAsync(token);

            return Unit.Value;
        }
        catch(ValidationException ex)
        {
            await _unitOfWork.RollBackAsync();
            return CsvParseErrors.Validation(ex.Message);
        }
        catch(InvalidOperationException ex)
        {
            await _unitOfWork.RollBackAsync();
            return UnitOfWorkErrors.TransactionNotStarted(ex.Message);
        }
        catch (Exception ex)
        {
            return Error.Failure("unexpected.processFileAndSaveData", "Unexpected error in processing data");
        }


    }
}
