using DbUp;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace VibeCrud.Infrastructure.Migrations;

public interface IMigrationRunner
{
    Task<bool> RunMigrationsAsync(string connectionString);
}

public class DbUpMigrationRunner : IMigrationRunner
{
    private readonly ILogger<DbUpMigrationRunner> _logger;

    public DbUpMigrationRunner(ILogger<DbUpMigrationRunner> logger)
    {
        _logger = logger;
    }

    public async Task<bool> RunMigrationsAsync(string connectionString)
    {
        try
        {
            _logger.LogInformation("Starting database migrations...");

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToAutodetectedLog()
                .Build();

            var result = await Task.Run(() => upgrader.PerformUpgrade());

            if (!result.Successful)
            {
                _logger.LogError(result.Error, "Database migration failed");
                return false;
            }

            _logger.LogInformation("Database migrations completed successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while running database migrations");
            return false;
        }
    }
}