using Chronos.Application.Common.Errors;

namespace Chronos.Application.Features.ResultsData.Errors;

public static class CsvParseErrors
{
    public static Error Validation(string message) =>
        Error.Validation("csv.validation", message);

    public static Error ParseError() =>
        Error.Failure("csv.parse", "Something went wrong in parsing csv file");
}
