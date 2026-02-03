using Chronos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence;

public class ChronosDbContext(DbContextOptions<ChronosDbContext> options)
    : DbContext(options)
{

    public DbSet<ValueEntity> ValueEntities { get; set; }
    public DbSet<ResultEntity> ResultEntities { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ChronosDbContext).Assembly);
    }
}
