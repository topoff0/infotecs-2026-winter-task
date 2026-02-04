namespace Chronos.Core.Entities;

public class ResultEntity
{
    public Guid Id { get; private set; }
    public double DeltaSeconds { get; private set; }
    public DateTime MinDate { get; private set; }
    public double AvgExecutionTime { get; private set; }
    public double AvgNumericValue { get; private set; }
    public double MedianNumericValue { get; private set; }
    public double MinNumericValue { get; private set; }
    public double MaxNumericValue { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public static ResultEntity Create(double deltaSeconds,
            DateTime minDate,
            double avgExecutionTime,
            double avgNumericValue,
            double medianNumericValue,
            double minNumericValue,
            double maxNumericValue,
            string fileName)
        => new()
        {
            Id = Guid.NewGuid(),
            DeltaSeconds = deltaSeconds,
            MinDate = minDate,
            AvgExecutionTime = avgExecutionTime,
            AvgNumericValue = avgNumericValue,
            MedianNumericValue = medianNumericValue,
            MinNumericValue = minNumericValue,
            MaxNumericValue = maxNumericValue,
            FileName = fileName
        };

}
