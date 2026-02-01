using Chronos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Infrastructure.Persistence;

public class ChronosDbContext(DbContextOptions<ChronosDbContext> options)
    : DbContext(options)
{

    public DbSet<Value> Values { get; set; }
    public DbSet<Result> Results { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ChronosDbContext).Assembly);
    }
}
