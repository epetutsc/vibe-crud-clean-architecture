# VibeCrud.Infrastructure

This project contains the infrastructure implementations for data access, messaging, and external services.

## Overview

The Infrastructure layer provides concrete implementations of the interfaces defined in the Domain layer. It handles all external concerns such as databases, messaging systems, and third-party services.

## Structure

### Data Access
- `VibeCrudDbContext`: Entity Framework Core database context
- `AddressRepository`: Implementation of `IAddressRepository` using Entity Framework Core

### Messaging
- `InMemoryEventBus`: In-memory implementation of `IEventBus` for domain events

### Configuration
- `DependencyInjection`: Extension methods for registering infrastructure services

## Key Features

### Entity Framework Core Integration
- **Database Context**: Configured for SQL Server with proper entity mappings
- **Advanced Querying**: Supports filtering, sorting, and pagination for large datasets
- **Optimized Indexes**: Database indexes for performance optimization
- **Migrations**: Support for database schema migrations

### Repository Implementation
The `AddressRepository` provides:
- **CRUD Operations**: Create, Read, Update, Delete operations
- **Soft Delete**: Addresses are marked as deleted rather than physically removed
- **Advanced Querying**: Complex filtering and sorting capabilities
- **Pagination**: Efficient pagination for large datasets
- **Performance Optimized**: Uses appropriate indexes and query patterns

### In-Memory Event Bus
The `InMemoryEventBus` provides:
- **Publish/Subscribe**: Simple pub/sub pattern for domain events
- **Thread-Safe**: Uses concurrent collections for thread safety
- **Type-Safe**: Generic methods ensure type safety
- **Asynchronous**: Supports async event handling

## Database Configuration

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VibeCrudDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Entity Configuration
The `Address` entity is configured with:
- Primary key on `Id`
- Required fields with appropriate lengths
- Indexes for performance (Email, Name, City, ZipCode)
- Unique constraint on Email
- Soft delete filter

## Dependencies

- **Microsoft.EntityFrameworkCore.SqlServer**: SQL Server provider for Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design**: Design-time tools for migrations
- **VibeCrud.Domain**: Domain interfaces and entities

## Performance Considerations

### Database Optimization
- **Indexes**: Strategic indexing for common query patterns
- **Pagination**: Server-side pagination to handle large datasets
- **Filtering**: Efficient WHERE clauses with indexed columns
- **Soft Delete**: Global query filters for soft-deleted entities

### Query Optimization
- **IQueryable**: Uses deferred execution for optimal database queries
- **Projection**: Only selects necessary columns
- **Async Operations**: All database operations are asynchronous

## Testing

Infrastructure components are tested through integration tests:
- **Testcontainers**: Uses Docker containers for real database testing
- **Entity Framework Testing**: Tests actual database operations
- **Performance Testing**: Validates query performance with large datasets

See `VibeCrud.Infrastructure.Tests` for comprehensive integration test examples.

## Usage

Register infrastructure services in the Web project:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

This registers:
- Entity Framework DbContext
- Repository implementations
- Event bus implementation