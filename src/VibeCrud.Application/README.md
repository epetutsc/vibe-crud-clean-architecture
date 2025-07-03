# VibeCrud.Application

This project contains the application services and use cases for the VibeCrud address management system.

## Overview

The Application layer orchestrates the execution of business use cases. It acts as a bridge between the presentation layer and the domain layer, coordinating the workflow of a business operation.

## Structure

### Services
- `IAddressService`: Interface defining address management operations
- `AddressService`: Implementation of address management use cases

### DTOs (Data Transfer Objects)
- `AddressDto`: Represents address data for read operations
- `CreateAddressDto`: Represents data needed to create a new address
- `UpdateAddressDto`: Represents data needed to update an existing address
- `AddressQueryDto`: Represents query parameters for filtering and pagination

### Common
- `PagedResult<T>`: Generic class for handling paginated results

## Key Features

### Address Management Use Cases
The `AddressService` implements the following use cases:
- **Create Address**: Creates a new address and publishes domain events
- **Update Address**: Updates existing address information
- **Delete Address**: Soft deletes an address
- **Get Address by ID**: Retrieves a single address
- **Get Paged Addresses**: Retrieves addresses with filtering, sorting, and pagination

### Domain Event Integration
The service publishes domain events for important business actions:
- `AddressCreatedEvent`: Published when a new address is created
- `AddressUpdatedEvent`: Published when an address is updated
- `AddressDeletedEvent`: Published when an address is deleted

### Data Mapping
Extension methods provide clean mapping between domain entities and DTOs:
- `Address.MapToDto()`: Converts domain entity to DTO

## Dependencies

- **VibeCrud.Domain**: References domain entities and interfaces
- **No external packages**: Keeps the application layer pure

## Patterns Used

### Repository Pattern
The service uses the repository pattern through `IAddressRepository` to abstract data access.

### CQRS-like Approach
Separate DTOs for commands (Create, Update) and queries provide clear separation of concerns.

### Domain Events
Events are published to allow for loose coupling and extensibility.

## Error Handling

The service includes proper error handling:
- Throws `InvalidOperationException` when trying to update/delete non-existent addresses
- Validates business rules before performing operations

## Testing

Application services are tested through unit tests using mocking frameworks:
- Mock repository and event bus dependencies
- Verify proper interaction with dependencies
- Test business logic and error scenarios

See `VibeCrud.Application.Tests` for comprehensive test examples.