using Npgsql;

namespace Discount.Grpc.Utilities;

public static class MigrationsUtilities
{
    public static IApplicationBuilder ApplyMigrations<TContext>(this IApplicationBuilder app, int? retry = 0)
    {
        int retryForAvailability = retry.Value;

        var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var applyMigrationsScope = serviceScopeFactory.CreateScope();

        var configuration = applyMigrationsScope.ServiceProvider.GetRequiredService<IConfiguration>();
        var logger = applyMigrationsScope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migration postgresql starting");

            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                        ProductName VARCHAR(24) NOT NULL,
                                                        Description TEXT,
                                                        Amount INT)";

            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();
            
            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postgresql database");
        }
        catch (NpgsqlException exception)
        {
            logger.LogError(exception, "An error occurred while migration the postgresql database");
            
            if (retryForAvailability < 10)
            {
                retryForAvailability++;
                Task.Delay(2000);
                ApplyMigrations<TContext>(app, retryForAvailability);
            }

            throw;
        }

        return app;
    }
}
