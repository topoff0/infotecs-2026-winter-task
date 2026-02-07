using Chronos.Application.Features.TimescaleData.DTOs.Filters;
using Microsoft.Extensions.Logging;

namespace Chronos.Application.Logger;

public static partial class GetResultsLogger
{
    [LoggerMessage(
        EventId = 200,
        Level = LogLevel.Information,
        Message = "Start getting results with following filters: {Filters}")]
    public static partial void LogStartGetResultsWithFilters(this ILogger logger, ResultFilters filters);

    [LoggerMessage(
        EventId = 201,
        Level = LogLevel.Debug,
        Message = "Successfully get {Count} results by following filters: {Filters}")]
    public static partial void LogSuccessfulGetFilteredResults(this ILogger logger, int count, ResultFilters filters);

    [LoggerMessage(
        EventId = 202,
        Level = LogLevel.Error,
        Message = "Unexpected error while getting filtered results: {Message}")]
    public static partial void LogGetFilteredResultsUnexpectedError(this ILogger logger, string message);

    [LoggerMessage(
        EventId = 203,
        Level = LogLevel.Information,
        Message = "Start getting ordered last results")]
    public static partial void LogStartGetOrderedLastResults(this ILogger logger);

    [LoggerMessage(
        EventId = 204,
        Level = LogLevel.Information,
        Message = "Successfully get last {Count} ordered results")]
    public static partial void LogSuccessfulGetLastOrderedResults(this ILogger logger, int count);

    [LoggerMessage(
        EventId = 205,
        Level = LogLevel.Error,
        Message = "Unexpected error while getting last ordered results: {Message}")]
    public static partial void LogGetLastOrderedResultsUnexpectedError(this ILogger logger, string message);
}
