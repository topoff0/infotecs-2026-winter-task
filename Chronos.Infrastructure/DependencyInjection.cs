using Chronos.Application.Features.TimescaleData.Commands; using Chronos.Application.Features.TimescaleData.Queries;
using Chronos.Application.Services;
using Chronos.Core.Repositories;
using Chronos.Core.Repositories.Common;
using Chronos.Infrastructure.Persistence;
using Chronos.Infrastructure.Persistence.Repositories;
using Chronos.Infrastructure.Persistence.Repositories.Common;
using Chronos.Infrastructure.Services;
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

        services.AddServices();

        return services;
    }

    public static async Task ApplyMigrationAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ChronosDbContext>();

        try
        {
            var pendingingMigraitons = await context.Database.GetPendingMigrationsAsync();

            if (pendingingMigraitons.Any())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    // ============================== PRIVATE ========================================

    private static IServiceCollection AddChronosDbContext(this IServiceCollection services,
                                                          IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ChronosDbConnection")
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
        services.AddScoped<IValueEntityRepository, ValueEntityRepository>();

        services.AddScoped<IResultEntityRepository, ResultEntityRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICsvParser, CsvParser>();

        services.AddScoped<IResultCalculator, ResultCalculator>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ProcessFileAndSaveDataCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetResultsWithFiltersQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetLastOrderedResultsQuery).Assembly);
        });

        return services;
    }
}
