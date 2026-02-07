using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;
using Chronos.Application.Features.TimescaleData.DTOs.Filters;

namespace Chronos.API.Contracts.Requests;

public sealed record GetResultsWithFiltersRequest(
    string? FileName,
    string? FirstOperationsStartedFrom,
    string? FirstOperationStartedTo,
    string? AvgNumericValueFrom,
    string? AvgNumericValueTo,
    string? AvgExecutionTimeFrom,
    string? AvgExecutionTimeTo)
{
    public ResultT<ResultFilters> ToResultFilters()
    {
        DateTime? fromDate = null;
        DateTime? toDate = null;

        if (!string.IsNullOrWhiteSpace(FirstOperationsStartedFrom))
        {
            if (!DateTime.TryParse(FirstOperationsStartedFrom, out var f))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_date",
                        "'FirstOperationsStartedFrom' is not a valid date."
                    )
                );
            }
            fromDate = f;
        }

        if (!string.IsNullOrWhiteSpace(FirstOperationStartedTo))
        {
            if (!DateTime.TryParse(FirstOperationStartedTo, out var t))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_date",
                        "'FirstOperationStartedTo' is not a valid date."
                    )
                );
            }
            toDate = t;
        }

        double? avgNumericFrom = null, avgNumericTo = null;
        if (!string.IsNullOrWhiteSpace(AvgNumericValueFrom))
        {
            if (!double.TryParse(AvgNumericValueFrom, out var nf))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_number",
                        "'AvgNumericValueFrom' is not a valid number."
                    )
                );
            }
            avgNumericFrom = nf;
        }

        if (!string.IsNullOrWhiteSpace(AvgNumericValueTo))
        {
            if (!double.TryParse(AvgNumericValueTo, out var nt))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_number",
                        "'AvgNumericValueTo' is not a valid number."
                    )
                );
            }
            avgNumericTo = nt;
        }

        double? avgExecFrom = null, avgExecTo = null;
        if (!string.IsNullOrWhiteSpace(AvgExecutionTimeFrom))
        {
            if (!double.TryParse(AvgExecutionTimeFrom, out var ef))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_number",
                        "'AvgExecutionTimeFrom' is not a valid number."
                    )
                );
            }
            avgExecFrom = ef;
        }

        if (!string.IsNullOrWhiteSpace(AvgExecutionTimeTo))
        {
            if (!double.TryParse(AvgExecutionTimeTo, out var et))
            {
                return ResultT<ResultFilters>.Failure(
                    Error.Failure(
                        "filters.invalid_number",
                        "'AvgExecutionTimeTo' is not a valid number."
                    )
                );
            }
            avgExecTo = et;
        }

        var filtersDto = new ResultFilters(
            FileName,
            fromDate,
            toDate,
            avgNumericFrom,
            avgNumericTo,
            avgExecFrom,
            avgExecTo
        );

        var validation = filtersDto.Validate();
        if (!validation.IsSuccess)
        {
            return ResultT<ResultFilters>.Failure(validation.Error!);
        }

        return ResultT<ResultFilters>.Success(filtersDto);
    }
}

