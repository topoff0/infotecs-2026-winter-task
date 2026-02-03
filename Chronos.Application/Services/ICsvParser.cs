using Chronos.Core.Entities;

namespace Chronos.Application.Services;

public interface ICsvParser
{
    Task<IReadOnlyList<Value>> Parse(Stream stream, CancellationToken token);
}
