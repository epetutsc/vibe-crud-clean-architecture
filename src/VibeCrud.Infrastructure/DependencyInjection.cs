using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeCrud.Infrastructure.SqlServer;
using VibeCrud.Infrastructure.Messaging;
using VibeCrud.Infrastructure.Migrations;

namespace VibeCrud.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add SQL Server infrastructure
        services.AddSqlServerInfrastructure(configuration);

        // Add messaging infrastructure
        services.AddMessagingInfrastructure();

        // Add migrations infrastructure
        services.AddMigrationsInfrastructure();

        return services;
    }
}