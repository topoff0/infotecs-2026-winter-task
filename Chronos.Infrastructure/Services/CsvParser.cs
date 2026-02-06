using System.ComponentModel.DataAnnotations;
using Chronos.Application.Services;
using Chronos.Core.Entities;

namespace Chronos.Infrastructure.Services;

public class CsvParser : ICsvParser
{

    public async Task<IReadOnlyList<ValueEntity>> Parse(string fileName, Stream stream, CancellationToken token)
    {
        var reader = new StreamReader(stream);

        var lines = (await reader.ReadToEndAsync(token)).Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 1 || lines.Length > 10_000)
            throw new ValidationException("Invalid lines count");

        var values = new List<ValueEntity>();

        foreach (var line in lines.Skip(1))
        {
            var parts = line.Split(';');

            if (parts.Length != 3)
                throw new ValidationException("Invalid format");

            if (!DateTime.TryParse(parts[0], out var date))
                throw new ValidationException("Invalid date format");

            if (date < new DateTime(2000, 1, 1) || date >= DateTime.UtcNow)
                throw new ValidationException("Invalid date range");

            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            if (!double.TryParse(parts[1], out var executionTime))
                throw new ValidationException("Invalid execution time format");

            if (executionTime < 0)
                throw new ValidationException("Invalid value of the execution time");

            if (!double.TryParse(parts[2], out var numericValue))
                throw new ValidationException("Invalid numeric value format");

            if (numericValue < 0)
                throw new ValidationException("Invalid value of the numeric value");

            values.Add(ValueEntity.Create(date, executionTime, numericValue, fileName));
        }

        return values;
    }
}
