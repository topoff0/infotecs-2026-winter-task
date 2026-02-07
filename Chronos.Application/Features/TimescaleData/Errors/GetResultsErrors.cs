using Chronos.Application.Common.Errors;

namespace Chronos.Application.Features.TimescaleData.Errors;

public static class GetResultsErrors
{
    public static Error GetFilteredResultsError() =>
        Error.Failure("results.getFiltered", "Failed to retrieve results");

    public static Error GetLastResultsByFileNameError() =>
        Error.Failure("results.getLastResults", "Failed to retrieve results");
}
