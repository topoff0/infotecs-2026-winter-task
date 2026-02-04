using Chronos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Infrastructure.Persistence.Configuration;

public class ResultEntityConfigurations : IEntityTypeConfiguration<ResultEntity>
{
    public void Configure(EntityTypeBuilder<ResultEntity> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.MaxNumericValue)
            .IsRequired();

        builder.Property(r => r.MinNumericValue)
            .IsRequired();

        builder.Property(r => r.MedianNumericValue)
            .IsRequired();

        builder.Property(r => r.AvgNumericValue)
            .IsRequired();

        builder.Property(r => r.AvgExecutionTime)
            .IsRequired();

        builder.Property(r => r.DeltaSeconds)
            .IsRequired();

        builder.Property(r => r.MinDate)
            .IsRequired();
    }
}
