using Chronos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Infrastructure.Persistence.Configuration;

public class ValueConfiguration : IEntityTypeConfiguration<Value>
{
    public void Configure(EntityTypeBuilder<Value> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.DateStart)
            .IsRequired();

        builder.Property(v => v.NumericValue)
            .IsRequired();

        builder.Property(v => v.ExecutionTime)
            .IsRequired();
    }
}
