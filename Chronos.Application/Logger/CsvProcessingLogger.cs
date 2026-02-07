using Microsoft.Extensions.Logging;

namespace Chronos.Application.Logger;

public static partial class CsvProcessingLogger
{
    [LoggerMessage(
        EventId = 100,
        Level = LogLevel.Information,
        Message = "Start processing CSV file {FileName}")]
    public static partial void LogStartProcessingCsvFile(this ILogger logger, string fileName);

    [LoggerMessage(
        EventId = 101,
        Level = LogLevel.Debug,
        Message = "Parsed {Count} values from file {FileName}")]
    public static partial void LogProcessingItems(this ILogger logger, int count, string fileName);

    [LoggerMessage(
        EventId = 102,
        Level = LogLevel.Information,
        Message = "File {FileName} processed successfully")]
    public static partial void LogSuccessfulProcessed(this ILogger logger, string fileName);

    [LoggerMessage(
        EventId = 103,
        Level = LogLevel.Warning,
        Message = "Validation failed while processing file {FileName}: {Message}")]
    public static partial void LogCsvFileValidationError(this ILogger logger, string fileName, string message);

    [LoggerMessage(
        EventId = 104,
        Level = LogLevel.Error,
        Message = "Unexpected error while processing file {FileName}: {Message}")]
    public static partial void LogCsvFileUnexpectedError(this ILogger logger, string fileName, string message);
}
