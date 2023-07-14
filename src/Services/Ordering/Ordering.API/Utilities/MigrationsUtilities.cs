using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Utilities;

public static class MigrationsUtilities
{
    public static IApplicationBuilder ApplyMigrations<TDbContext>(this IApplicationBuilder app, Action<TDbContext, IServiceProvider> seeder, int? retry = 0)
        where TDbContext : DbContext
    {
        int retryForAvailability = retry.Value;

        var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var applyMigrationsScope = serviceScopeFactory.CreateScope();

        var logger = applyMigrationsScope.ServiceProvider.GetRequiredService<ILogger<TDbContext>>();
        var dbContext = applyMigrationsScope.ServiceProvider.GetService<TDbContext>();

        if (dbContext is null)
        {
            throw new ArgumentNullException("Database is not available");
        }

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TDbContext).Name);
            var pendingMigrations = dbContext.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
            {
                MigrateAndSeed(seeder, dbContext, applyMigrationsScope.ServiceProvider);
            }

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TDbContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TDbContext).Name);

            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                System.Threading.Thread.Sleep(2000);
                ApplyMigrations(app, seeder, retryForAvailability);
            }
        }

        return app;
    }

    private static void MigrateAndSeed<TDbContext>
    (
        Action<TDbContext, IServiceProvider> seeder,
        TDbContext context,
        IServiceProvider services
    )
        where TDbContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}