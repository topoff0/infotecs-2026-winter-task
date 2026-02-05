using Chronos.Core.Entities;

namespace Chronos.Application.Services;

public interface ICsvParser
{
    Task<IReadOnlyList<ValueEntity>> Parse(string fileName, Stream stream, CancellationToken token);
}
