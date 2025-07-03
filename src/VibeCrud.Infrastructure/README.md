# VibeCrud.Infrastructure

# VibeCrud.Infrastructure

This project serves as the main infrastructure orchestration layer that coordinates all infrastructure concerns.

## Overview

The Infrastructure layer provides a unified entry point for all infrastructure services while maintaining separation of concerns through modular sub-projects. This design allows for better maintainability and easier testing of individual infrastructure components.

## Modular Architecture

### Infrastructure Sub-Projects
- **VibeCrud.Infrastructure.SqlServer**: SQL Server and Entity Framework Core implementations
- **VibeCrud.Infrastructure.Messaging**: Event bus and messaging implementations

### Benefits of Modular Design
- **Single Responsibility**: Each project handles one specific infrastructure concern
- **Easier Testing**: Components can be tested in isolation
- **Flexible Deployment**: Individual components can be replaced or upgraded independently
- **Reduced Dependencies**: Projects only reference what they actually need

## Structure

### Main Infrastructure Project
- `DependencyInjection`: Orchestrates registration of all infrastructure services
- Acts as a facade that combines all infrastructure sub-projects

### SqlServer Sub-Project (`VibeCrud.Infrastructure.SqlServer`)
- Entity Framework Core database context
- Repository implementations
- Database schema configuration
- Migration support

### Messaging Sub-Project (`VibeCrud.Infrastructure.Messaging`)
- Event bus implementations
- Domain event handling
- Pub/Sub messaging patterns

## Key Features

### Unified Configuration
The main infrastructure project provides a single entry point:

```csharp
services.AddInfrastructure(configuration);
```

This internally calls:
- `AddSqlServerInfrastructure(configuration)` - Database and repositories
- `AddMessagingInfrastructure()` - Event bus and messaging

### Modular Dependencies
Each sub-project only references what it needs:
- **SqlServer**: Only SQL Server and Entity Framework dependencies
- **Messaging**: Only messaging-related dependencies
- **Main**: Orchestrates the sub-projects

## Dependencies

### Direct Dependencies
- **VibeCrud.Infrastructure.SqlServer**: SQL Server infrastructure
- **VibeCrud.Infrastructure.Messaging**: Messaging infrastructure
- **Microsoft.Extensions.DependencyInjection**: Dependency injection

### Transitive Dependencies
Through sub-projects:
- Entity Framework Core (via SqlServer project)
- SQL Server provider (via SqlServer project)
- Domain layer (via sub-projects)

## Configuration

### Connection Strings
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VibeCrudDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Service Registration
```csharp
// In Program.cs or Startup.cs
builder.Services.AddInfrastructure(builder.Configuration);
```

This registers all infrastructure services:
- Database context and repositories
- Event bus and messaging
- All required dependencies

## Testing Strategy

Infrastructure testing is distributed across projects:
- **VibeCrud.Infrastructure.Tests**: Integration tests for the complete infrastructure
- Individual sub-projects can have their own focused tests
- Testcontainers for real database integration testing

## Future Extensibility

The modular design allows for easy addition of new infrastructure concerns:
- **VibeCrud.Infrastructure.Email**: Email service implementations
- **VibeCrud.Infrastructure.FileStorage**: File storage implementations
- **VibeCrud.Infrastructure.Caching**: Caching implementations
- **VibeCrud.Infrastructure.Logging**: Structured logging implementations

Each new concern can be added as a separate project with its own dependencies and configuration.

## Usage

Simply reference the main Infrastructure project and call:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

The infrastructure layer will handle all the complexity of coordinating the various infrastructure concerns while maintaining clean separation and modularity.