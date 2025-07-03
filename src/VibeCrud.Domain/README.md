# VibeCrud.Domain

This project contains the core domain entities and business logic for the VibeCrud address management system.

## Overview

The Domain layer is the heart of the Clean Architecture implementation. It contains:

- **Entities**: Core business objects that represent the main concepts of the application
- **Domain Interfaces**: Contracts that define how the domain interacts with external concerns
- **Business Logic**: Core business rules and domain-specific logic

## Structure

### Entities
- `BaseEntity`: Abstract base class providing common properties for all entities
- `Address`: Main entity representing an address with all related properties

### Interfaces
- `IAddressRepository`: Contract for address data access operations
- `IEventBus`: Contract for domain event publishing and handling

## Key Features

### Address Entity
The `Address` entity includes:
- Personal information (FirstName, LastName)
- Address details (Street, HouseNumber, ZipCode, City, Country)
- Contact information (Email, Phone)
- Computed properties (FullName, FullAddress)
- Audit properties (CreatedAt, UpdatedAt, IsDeleted)

### Domain Design Principles
- **Encapsulation**: Entities encapsulate their own business logic
- **Invariants**: Business rules are enforced within the domain
- **Value Objects**: Computed properties provide derived values
- **Domain Events**: Events are published for important business actions

## Dependencies

This project has no external dependencies and relies only on .NET Core base classes. This ensures that the domain remains pure and independent of infrastructure concerns.

## Usage

The domain entities are used by the Application layer to implement business use cases. The interfaces defined here are implemented by the Infrastructure layer to provide concrete implementations for data access and external services.

## Testing

Domain logic is tested through unit tests that verify:
- Entity behavior and computed properties
- Business rule enforcement
- Domain logic correctness

See `VibeCrud.Domain.Tests` for comprehensive test examples.