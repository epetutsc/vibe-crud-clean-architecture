using Microsoft.Extensions.DependencyInjection;
using VibeCrud.Domain.Interfaces;

namespace VibeCrud.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessagingInfrastructure(this IServiceCollection services)
    {
        // Add messaging
        services.AddSingleton<IEventBus, InMemoryEventBus>();

        return services;
    }
}