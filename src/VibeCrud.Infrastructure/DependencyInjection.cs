using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeCrud.Domain.Interfaces;
using VibeCrud.Infrastructure.Data;
using VibeCrud.Infrastructure.Messaging;
using VibeCrud.Infrastructure.Repositories;

namespace VibeCrud.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Entity Framework
        services.AddDbContext<VibeCrudDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(VibeCrudDbContext).Assembly.FullName)));

        // Add repositories
        services.AddScoped<IAddressRepository, AddressRepository>();

        // Add messaging
        services.AddSingleton<IEventBus, InMemoryEventBus>();

        return services;
    }
}