using Chronos.Core.Entities;

namespace Chronos.Application.Services;

public interface ICsvParser
{
    Task<IReadOnlyList<ValueEntity>> Parse(Stream stream, CancellationToken token);
}
