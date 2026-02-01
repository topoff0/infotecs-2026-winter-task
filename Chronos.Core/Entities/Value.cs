namespace Chronos.Core.Entities;

public class Value
{
    public Guid Id { get; private set; }
    public DateTime DateStart { get; private set; }
    public double ExecutionTime { get; private set; }
    public double NumericValue { get; private set; }

    public static Value Create(DateTime dateStart,
                               double executionTime,
                               double numericValue)
        => new()
        {
            Id = Guid.NewGuid(),
            DateStart = dateStart,
            ExecutionTime = executionTime,
            NumericValue = numericValue
        };

    public void Update(DateTime? dateStart = null,
                       double? executionTime = null,
                       double? numericValue = null)
    {
        if (dateStart.HasValue)
            DateStart = dateStart.Value;
        if (executionTime.HasValue)
            ExecutionTime = executionTime.Value;
        if (numericValue.HasValue)
            NumericValue = numericValue.Value;
    }
}
