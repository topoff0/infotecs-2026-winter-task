using Chronos.Core.Repositories;
using Chronos.Infrastructure.Persistence;
using Chronos.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        services.AddChronosDbContext(configuration);

        services.AddRepositories();

        return services;
    }

    // ============================== PRIVATE ========================================

    private static IServiceCollection AddChronosDbContext(this IServiceCollection services,
                                                          IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ChronosConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'ChronosConnection' is not configured");

        services.AddDbContext<ChronosDbContext>(options =>
        {
            options.UseNpgsql(connectionString, pgOpt =>
            {
                pgOpt.EnableRetryOnFailure(maxRetryCount: 5,
                                           maxRetryDelay: TimeSpan.FromSeconds(15),
                                           errorCodesToAdd: null);
            });
        });

        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IValueRepository, ValueRepository>();

        services.AddScoped<IResultRepository, ResultRepository>();

        return services;
    }
}
