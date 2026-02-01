namespace Chronos.Core.Entities;

public class Result
{
    public Guid Id { get; private set; }
    public double DeltaDate { get; private set; }
    public DateTime MinDate { get; private set; }
    public double AvgExecutionTime { get; private set; }
    public double AvgNumericValue { get; private set; }
    public double MedianNumericValue { get; private set; }
    public double MinNumericValue { get; private set; }
    public double MaxNumericValue { get; private set; }


    public static Result Create(double deltaDate,
            DateTime minDate,
            double avgExecutionTime,
            double avgNumericValue,
            double medianNumericValue,
            double minNumericValue,
            double maxNumericValue)
        => new()
        {
            Id = Guid.NewGuid(),
            DeltaDate = deltaDate,
            MinDate = minDate,
            AvgExecutionTime = avgExecutionTime,
            AvgNumericValue = avgNumericValue,
            MedianNumericValue = medianNumericValue,
            MinNumericValue = minNumericValue,
            MaxNumericValue = maxNumericValue
        };

}
