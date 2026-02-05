using Chronos.Application.Features.ResultsData.DTOs.Requests;
using Chronos.Application.Services;
using Chronos.Core.Entities;

namespace Chronos.Infrastructure.Services;

public class ResultCalculator : IResultCalculator
{
    public ResultEntity Calculate(CalculateResultDto dto)
    {
        var minDate = dto.Values.Min(v => v.DateStart);
        var maxDate = dto.Values.Max(v => v.DateStart);

        return ResultEntity.Create(
        fileName: dto.FileName,
        deltaSeconds: (maxDate - minDate).TotalSeconds,
        minDate: minDate,
        avgExecutionTime: dto.Values.Average(v => v.ExecutionTime),
        avgNumericValue: dto.Values.Average(v => v.NumericValue),
        medianNumericValue: Median(dto.Values.Select(v => v.NumericValue)),
        maxNumericValue: dto.Values.Max(v => v.NumericValue),
        minNumericValue: dto.Values.Min(v => v.NumericValue)
        );
    }

    private static double Median(IEnumerable<double> source)
    {
        var sorted = source.OrderBy(x => x).ToArray();

        var mid = sorted.Length / 2;

        return mid % 2 == 0
            ? (sorted[mid - 1] + sorted[mid]) / 2
            : sorted[mid];
    }
}
