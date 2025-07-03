using VibeCrud.Domain.Interfaces;
using System.Collections.Concurrent;

namespace VibeCrud.Infrastructure.Messaging;

public class InMemoryEventBus : IEventBus
{
    private readonly ConcurrentDictionary<Type, List<Func<object, Task>>> _handlers = new();

    public async Task PublishAsync<T>(T @event) where T : class
    {
        if (_handlers.TryGetValue(typeof(T), out var handlers))
        {
            var tasks = handlers.Select(handler => handler(@event));
            await Task.WhenAll(tasks);
        }
    }

    public Task SubscribeAsync<T>(Func<T, Task> handler) where T : class
    {
        _handlers.AddOrUpdate(typeof(T), 
            new List<Func<object, Task>> { evt => handler((T)evt) },
            (key, existingHandlers) =>
            {
                existingHandlers.Add(evt => handler((T)evt));
                return existingHandlers;
            });
        
        return Task.CompletedTask;
    }
}