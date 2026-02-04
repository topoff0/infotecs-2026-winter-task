using Chronos.Application.Features.ResultsData.DTOs.Requests;
using Chronos.Application.Services;
using Chronos.Core.Entities;

namespace Chronos.Infrastructure.Services;

public class ResultCalculator : IResultCalculator
{
    public ResultEntity Calculate(CalculateResultRequest request)
    {
        var minDate = request.Values.Min(v => v.DateStart);
        var maxDate = request.Values.Max(v => v.DateStart);

        return ResultEntity.Create(
        fileName: request.FileName,
        deltaSeconds: (maxDate - minDate).TotalSeconds,
        minDate: minDate,
        avgExecutionTime: request.Values.Average(v => v.ExecutionTime),
        avgNumericValue: request.Values.Average(v => v.NumericValue),
        medianNumericValue: Median(request.Values.Select(v => v.NumericValue)),
        maxNumericValue: request.Values.Max(v => v.NumericValue),
        minNumericValue: request.Values.Min(v => v.NumericValue)
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
