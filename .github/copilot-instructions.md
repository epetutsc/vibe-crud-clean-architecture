# VibeCrud Clean Architecture - Address Management System

**ALWAYS follow these instructions first and only fall back to additional search or bash commands when the information here is incomplete or found to be in error.**

VibeCrud is a modern address management system built with .NET 9 Clean Architecture principles, using Blazor Server, Entity Framework Core, and SQL Server. It demonstrates complete CRUD operations with advanced features like server-side pagination, filtering, and real-time updates.

## Working Effectively

### Prerequisites and Setup
**CRITICAL**: This application requires .NET 9 SDK. .NET 8 will NOT work.

Install required tools in this exact order:
```bash
# Install .NET 9 SDK (version 9.0.101 or later)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 9.0.101
export PATH="/home/runner/.dotnet:$PATH"

# Verify .NET 9 installation
dotnet --version  # Must show 9.0.101 or higher

# Install Aspire workload (required for orchestration)
dotnet workload install aspire
```

### Building and Testing
**NEVER CANCEL builds or tests. Set appropriate timeouts.**

Bootstrap and validate the repository:
```bash
# Restore packages - takes ~25 seconds first time. NEVER CANCEL. Set timeout to 300+ seconds.
dotnet restore

# Build solution - takes ~13 seconds. NEVER CANCEL. Set timeout to 120+ seconds.
dotnet build

# Run all tests - takes ~49 seconds including Docker containers. NEVER CANCEL. Set timeout to 1800+ seconds.
dotnet test
```

**Expected Timing:**
- Package restore: 25 seconds (first time only)
- Build: 13 seconds  
- Tests: 49 seconds (includes integration tests with Docker SQL Server containers)

### Running the Application

**Option A: .NET Aspire Orchestration (Recommended)**
```bash
# Run with full orchestration - automatically starts SQL Server container and web app
dotnet run --project src/VibeCrud.AppHost
```
- Provides Aspire dashboard at http://localhost:15000
- Automatically configures SQL Server in Docker
- Handles service discovery and configuration
- **NOTE**: May have connectivity issues in restricted environments

**Option B: Traditional Development**
```bash
# Requires SQL Server LocalDB or SQL Server instance
dotnet run --project src/VibeCrud.Web
```
- Requires connection string configuration in appsettings.json
- Uses DefaultConnection: "Server=(localdb)\\MSSQLLocalDB;Database=VibeCrudDb;Trusted_Connection=true"

## Validation

### Manual Validation Scenarios
After making changes, ALWAYS test these core scenarios:

1. **Basic CRUD Operations**:
   - Navigate to /Addresses page
   - Create a new address with all required fields
   - Edit an existing address
   - Delete an address (soft delete)
   - Verify changes persist

2. **Data Grid Functionality**:
   - Test search/filtering across all fields
   - Test column sorting (ascending/descending)
   - Test pagination with large datasets
   - Verify responsive design on different screen sizes

3. **Form Validation**:
   - Try submitting empty required fields
   - Test email format validation
   - Verify client-side validation messages

### Testing Requirements
Always run these validation steps before completing changes:
```bash
# Run all tests - NEVER CANCEL. Wait 1800+ seconds if needed.
dotnet test --no-build

# Run specific test projects if needed
dotnet test tests/VibeCrud.Domain.Tests --no-build
dotnet test tests/VibeCrud.Application.Tests --no-build
dotnet test tests/VibeCrud.Infrastructure.Tests --no-build  # Uses Docker containers
dotnet test tests/VibeCrud.Web.Tests --no-build
```

**Test Summary**: 22 total tests covering all layers. Integration tests use TestContainers for SQL Server.

## Architecture Overview

### Clean Architecture Layers
```
┌─────────────────────────────────────┐
│           Presentation              │
│         (VibeCrud.Web)              │  ← Blazor Server UI
├─────────────────────────────────────┤
│          Application                │
│      (VibeCrud.Application)         │  ← Use cases, Services, DTOs
├─────────────────────────────────────┤
│          Infrastructure             │
│     (VibeCrud.Infrastructure)       │  ← EF Core, Repositories, Events
├─────────────────────────────────────┤
│            Domain                   │
│        (VibeCrud.Domain)            │  ← Entities, Interfaces
└─────────────────────────────────────┘
```

### Key Projects Location Guide

**Domain Layer** (`src/VibeCrud.Domain/`):
- Core business entities (Address)
- Domain interfaces (IAddressRepository, IEventBus)
- Domain events

**Application Layer** (`src/VibeCrud.Application/`):
- Use cases and business logic (AddressService)
- DTOs and mapping
- Application interfaces

**Infrastructure Layer**:
- `src/VibeCrud.Infrastructure/`: Core infrastructure, EF Core DbContext
- `src/VibeCrud.Infrastructure.SqlServer/`: SQL Server specific implementations
- `src/VibeCrud.Infrastructure.Messaging/`: In-memory event bus
- `src/VibeCrud.Infrastructure.Migrations/`: DbUp migration scripts (NOT EF Core migrations)

**Web Layer** (`src/VibeCrud.Web/`):
- Blazor Server components
- Pages and routing
- Custom data grid component

**Orchestration**:
- `src/VibeCrud.AppHost/`: .NET Aspire orchestration
- `src/VibeCrud.ServiceDefaults/`: Shared Aspire configuration

### Important Implementation Details

**Database Migrations**: Uses DbUp (NOT Entity Framework migrations)
- Migration scripts: `src/VibeCrud.Infrastructure.Migrations/Scripts/`
- Run automatically on application startup
- Check `DbUpMigrationRunner.cs` for migration logic

**Event Handling**: In-memory event bus
- Located in `VibeCrud.Infrastructure.Messaging`
- Domain events are handled synchronously within same process

**Data Grid**: Custom implementation (not Telerik Kendo)
- Server-side pagination and filtering
- Located in Web project components
- Optimized for large datasets

## Common Tasks

### Adding New Features
1. Start in Domain layer - define entities and interfaces
2. Add business logic in Application layer services
3. Implement data access in Infrastructure layer
4. Create UI components in Web layer
5. Always add corresponding tests in each layer

### Database Schema Changes
1. Create new SQL script in `src/VibeCrud.Infrastructure.Migrations/Scripts/`
2. Use sequential numbering (001_, 002_, etc.)
3. Scripts are embedded resources and run by DbUp
4. Test migrations against real SQL Server database

### Debugging Tips
- Check Aspire dashboard for service health and logs
- Integration tests provide examples of proper configuration
- Use `dotnet test` with specific test project for faster iteration
- Check `appsettings.json` for connection string configuration

## Limitations and Workarounds

**Environment Restrictions**:
- Aspire orchestration may fail in restricted network environments
- Fallback to traditional development with local SQL Server
- Integration tests require Docker for SQL Server containers

**Database Requirements**:
- SQL Server LocalDB for traditional development
- Docker for Aspire orchestration and integration tests
- No in-memory database option currently available

**Performance**:
- Build times include Blazor component compilation
- Test times include Docker container startup/teardown
- First package restore downloads all dependencies

## Repository Structure Reference
```
VibeCrud/
├── src/
│   ├── VibeCrud.Domain/           # Core business logic and entities
│   ├── VibeCrud.Application/      # Use cases and services  
│   ├── VibeCrud.Infrastructure/   # Data access and external services
│   ├── VibeCrud.Infrastructure.SqlServer/ # SQL Server implementations
│   ├── VibeCrud.Infrastructure.Messaging/ # In-memory event bus
│   ├── VibeCrud.Infrastructure.Migrations/ # DbUp migration scripts
│   ├── VibeCrud.Web/             # Blazor Server web application
│   ├── VibeCrud.AppHost/         # .NET Aspire orchestration
│   └── VibeCrud.ServiceDefaults/ # Shared Aspire configuration
├── tests/
│   ├── VibeCrud.Domain.Tests/     # Domain layer unit tests
│   ├── VibeCrud.Application.Tests/ # Application layer unit tests  
│   ├── VibeCrud.Infrastructure.Tests/ # Infrastructure integration tests
│   └── VibeCrud.Web.Tests/       # Web layer tests
└── VibeCrud.sln                  # Solution file
```

Always reference this guide first. Search or explore the codebase only when these instructions don't cover your specific scenario.