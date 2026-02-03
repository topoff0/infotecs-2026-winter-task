using Chronos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Infrastructure.Persistence.Configuration;

public class ValueEntityConfiguration : IEntityTypeConfiguration<ValueEntity>
{
    public void Configure(EntityTypeBuilder<ValueEntity> builder)
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
