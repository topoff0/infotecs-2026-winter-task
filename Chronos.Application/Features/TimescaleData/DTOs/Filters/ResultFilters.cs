using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;

namespace Chronos.Application.Features.TimescaleData.DTOs.Filters;

public record ResultFilters(
    string? FileName,
    DateTime? FirstOperationsStartedFrom,
    DateTime? FirstOperationStartedTo,
    double? AvgNumericValueFrom,
    double? AvgNumericValueTo,
    double? AvgExecutionTimeFrom,
    double? AvgExecutionTimeTo)
{
    public Result Validate()
    {
        if (FirstOperationsStartedFrom.HasValue && FirstOperationStartedTo.HasValue &&
            FirstOperationsStartedFrom > FirstOperationStartedTo)
        {
            return Result.Failure(Error.Failure(
                "filters.invalid_date_range",
                "'FirstOperationsStartedFrom' must be less than or equal to 'FirstOperationStartedTo'.")
            );
        }

        if (AvgNumericValueFrom.HasValue && AvgNumericValueTo.HasValue &&
            AvgNumericValueFrom > AvgNumericValueTo)
        {
            return Result.Failure(Error.Failure(
                "filters.invalid_numeric_range",
                "'AvgNumericValueFrom' must be less than or equal to 'AvgNumericValueTo'.")
            );
        }

        if (AvgExecutionTimeFrom.HasValue && AvgExecutionTimeTo.HasValue &&
            AvgExecutionTimeFrom > AvgExecutionTimeTo)
        {
            return Result.Failure(Error.Failure(
                "filters.invalid_execution_time_range",
                "'AvgExecutionTimeFrom' must be less than or equal to 'AvgExecutionTimeTo'.")
            );
        }

        return Result.Success();
    }
}

