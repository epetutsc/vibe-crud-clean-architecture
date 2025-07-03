using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeCrud.Domain.Interfaces;
using VibeCrud.Infrastructure.SqlServer.Data;
using VibeCrud.Infrastructure.SqlServer.Repositories;

namespace VibeCrud.Infrastructure.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Entity Framework
        services.AddDbContext<VibeCrudDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(VibeCrudDbContext).Assembly.FullName)));

        // Add repositories
        services.AddScoped<IAddressRepository, AddressRepository>();

        return services;
    }
}