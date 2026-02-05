using Chronos.Application.Common.Errors;

namespace Chronos.Application.Features.ResultsData.Errors;

public static class UnitOfWorkErrors
{
    public static Error TransactionNotStarted(string message) =>
        Error.Failure("transaction.notStarted", message);
}
