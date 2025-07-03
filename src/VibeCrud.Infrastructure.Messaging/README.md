# VibeCrud.Infrastructure.Messaging

This project contains messaging infrastructure implementations for the VibeCrud application.

## Purpose

This project provides concrete implementations for event-driven communication within the application. It is separated from other infrastructure concerns to maintain a modular architecture and allow for easy replacement of messaging implementations.

## Contents

### Event Bus
- **InMemoryEventBus**: Thread-safe in-memory implementation of the event bus interface
- **Event Publishing**: Async event publishing with support for multiple subscribers
- **Event Subscription**: Type-safe event subscription mechanism

### Features
- **Thread-Safe**: Uses concurrent collections for safe multi-threaded access
- **Type-Safe**: Generic event handling with compile-time type checking
- **Async Support**: Full async/await support for event publishing and handling
- **Multiple Subscribers**: Support for multiple handlers per event type

## Dependencies

- **VibeCrud.Domain**: References domain interfaces (IEventBus)
- **Microsoft.Extensions.DependencyInjection**: Dependency injection support

## Configuration

Use the `AddMessagingInfrastructure` extension method to register messaging services:

```csharp
services.AddMessagingInfrastructure();
```

This registers the InMemoryEventBus as a singleton service implementing IEventBus.

## Usage

### Publishing Events

```csharp
public class AddressService
{
    private readonly IEventBus _eventBus;
    
    public async Task CreateAddressAsync(CreateAddressDto dto)
    {
        // ... create address logic
        
        await _eventBus.PublishAsync(new AddressCreatedEvent(address.Id));
    }
}
```

### Subscribing to Events

```csharp
public class EmailNotificationHandler
{
    public async Task InitializeAsync(IEventBus eventBus)
    {
        await eventBus.SubscribeAsync<AddressCreatedEvent>(HandleAddressCreated);
    }
    
    private async Task HandleAddressCreated(AddressCreatedEvent @event)
    {
        // Handle the event
        await SendWelcomeEmailAsync(@event.AddressId);
    }
}
```

## Implementation Notes

- The current implementation is in-memory and will not persist events across application restarts
- Events are processed synchronously within the publishing thread
- For production scenarios, consider implementing a persistent event store or message broker
- The implementation supports multiple handlers per event type
- All event handling is async to support I/O operations

## Future Enhancements

This modular design allows for easy replacement with other messaging implementations such as:
- RabbitMQ
- Azure Service Bus
- Amazon SQS
- Apache Kafka
- Redis Pub/Sub