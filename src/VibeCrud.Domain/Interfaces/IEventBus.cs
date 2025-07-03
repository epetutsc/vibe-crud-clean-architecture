namespace VibeCrud.Domain.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>(T @event) where T : class;
    Task SubscribeAsync<T>(Func<T, Task> handler) where T : class;
}