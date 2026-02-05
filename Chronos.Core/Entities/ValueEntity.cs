namespace Chronos.Core.Entities;

public class ValueEntity
{
    public Guid Id { get; private set; }
    public DateTime DateStart { get; private set; }
    public double ExecutionTime { get; private set; }
    public double NumericValue { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public static ValueEntity Create(DateTime dateStart,
                               double executionTime,
                               double numericValue,
                               string fileName)
        => new()
        {
            Id = Guid.NewGuid(),
            DateStart = dateStart,
            ExecutionTime = executionTime,
            NumericValue = numericValue,
            FileName = fileName
        };
}
