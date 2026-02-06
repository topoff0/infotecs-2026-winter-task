namespace Chronos.Application.Features.TimescaleData.DTOs.Filters;

public sealed record ResultFilter(string? FileName,
                                  DateTime? FirstOperationsStartedFrom,
                                  DateTime? FirstOperationStartedTo,
                                  double? AvgNumericValueFrom,
                                  double? AvgNumericValueTo,
                                  double? AvgExecutionTimeFrom,
                                  double? AvgExecutionTimeTo);
