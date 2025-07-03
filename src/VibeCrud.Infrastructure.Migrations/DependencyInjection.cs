using Microsoft.Extensions.DependencyInjection;

namespace VibeCrud.Infrastructure.Migrations;

public static class DependencyInjection
{
    public static IServiceCollection AddMigrationsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMigrationRunner, DbUpMigrationRunner>();
        
        return services;
    }
}