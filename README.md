# VibeCrud - Clean Architecture Address Management System

A modern address management system built with Clean Architecture principles using .NET 8, Blazor Server, Entity Framework Core, and SQL Server.

## Overview

This project demonstrates a complete Clean Architecture implementation for managing address data. It includes advanced features like server-side pagination, filtering, sorting, and real-time updates, designed to handle large datasets efficiently.

## Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────┐
│           Presentation              │
│         (VibeCrud.Web)              │
│          Blazor Server              │
├─────────────────────────────────────┤
│          Application                │
│      (VibeCrud.Application)         │
│     Services, DTOs, UseCases        │
├─────────────────────────────────────┤
│          Infrastructure             │
│     (VibeCrud.Infrastructure)       │
│    EF Core, Repositories, Events    │
├─────────────────────────────────────┤
│            Domain                   │
│        (VibeCrud.Domain)            │
│      Entities, Interfaces           │
└─────────────────────────────────────┘
```

### Key Principles Applied
- **Dependency Inversion**: High-level modules don't depend on low-level modules
- **Single Responsibility**: Each class has one reason to change
- **Open/Closed**: Open for extension, closed for modification
- **Interface Segregation**: Clients depend only on interfaces they use
- **Domain-Driven Design**: Rich domain model with business logic

## Technology Stack

### Backend
- **.NET 8**: Latest version of .NET
- **Entity Framework Core**: ORM for data access
- **SQL Server**: Primary database
- **Clean Architecture**: Architectural pattern

### Frontend
- **Blazor Server**: Interactive web UI framework
- **Bootstrap 5**: CSS framework for responsive design
- **Custom Data Grid**: High-performance grid component

### Testing
- **xUnit**: Testing framework
- **Moq**: Mocking framework
- **Testcontainers.Net**: Integration testing with Docker
- **ASP.NET Core Testing**: Web application testing

### Infrastructure
- **In-Memory Event Bus**: Domain event handling
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Built-in DI container
- **.NET Aspire**: Cloud-native orchestration and development

### Development & Orchestration
- **.NET Aspire**: Local development orchestration
- **Docker**: Containerization and SQL Server hosting
- **OpenTelemetry**: Distributed tracing and monitoring
- **Health Checks**: Built-in application health monitoring

## Features

### Core Functionality
- ✅ **CRUD Operations**: Create, Read, Update, Delete addresses
- ✅ **Advanced Search**: Filter addresses across all fields
- ✅ **Sorting**: Sort by any column (ascending/descending)
- ✅ **Pagination**: Handle large datasets efficiently
- ✅ **Soft Delete**: Addresses are marked as deleted, not physically removed
- ✅ **Real-time Updates**: Blazor Server provides instant UI updates

### Data Grid Features
- ✅ **Server-side Processing**: Optimized for large datasets
- ✅ **Responsive Design**: Works on desktop, tablet, and mobile
- ✅ **Loading States**: Visual feedback during operations
- ✅ **Error Handling**: User-friendly error messages
- ✅ **Form Validation**: Client-side validation

### Architecture Features
- ✅ **Domain Events**: Loose coupling through event-driven architecture
- ✅ **Clean Separation**: Clear boundaries between layers
- ✅ **Testability**: Comprehensive unit and integration tests
- ✅ **Extensibility**: Easy to add new features and entities

## Project Structure

```
VibeCrud/
├── src/
│   ├── VibeCrud.Domain/           # Core business logic and entities
│   ├── VibeCrud.Application/      # Use cases and services
│   ├── VibeCrud.Infrastructure/   # Data access and external services
│   ├── VibeCrud.Web/             # Blazor Server web application
│   ├── VibeCrud.AppHost/         # .NET Aspire orchestration host
│   └── VibeCrud.ServiceDefaults/ # Shared Aspire service configuration
├── tests/
│   ├── VibeCrud.Domain.Tests/     # Domain layer unit tests
│   ├── VibeCrud.Application.Tests/ # Application layer unit tests
│   ├── VibeCrud.Infrastructure.Tests/ # Infrastructure integration tests
│   └── VibeCrud.Web.Tests/       # Web layer tests
└── VibeCrud.sln                  # Solution file
```

## Getting Started

### Prerequisites
- **.NET 8 SDK**
- **SQL Server** or **SQL Server LocalDB** (for standalone development)
- **Docker** (for integration tests and Aspire orchestration)
- **.NET Aspire workload** (for simplified local development)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/epetutsc/vibe-crud-clean-architecture.git
   cd vibe-crud-clean-architecture
   ```

2. **Install .NET Aspire workload (recommended for local development)**
   ```bash
   dotnet workload install aspire
   ```

3. **Choose your development approach:**

   **Option A: Using .NET Aspire (Recommended)**
   ```bash
   # Run the application with full orchestration (SQL Server in Docker + Web App)
   dotnet run --project src/VibeCrud.AppHost
   ```
   This will:
   - Start SQL Server in a Docker container
   - Automatically configure connection strings
   - Run database migrations
   - Start the web application
   - Provide a dashboard at http://localhost:15000

   **Option B: Traditional development**
   ```bash
   # Restore packages
   dotnet restore
   
   # Update connection string if needed in src/VibeCrud.Web/appsettings.json
   
   # Run the application (requires SQL Server/LocalDB)
   dotnet run --project src/VibeCrud.Web
   ```

### Running with .NET Aspire

.NET Aspire provides a streamlined local development experience:

1. **Start the orchestration**:
   ```bash
   dotnet run --project src/VibeCrud.AppHost
   ```

2. **Access the Aspire dashboard** at `http://localhost:15000` to:
   - Monitor application health and logs
   - View database connection status
   - Access the web application
   - Monitor telemetry and traces

3. **The web application** will be available through the dashboard or directly at the port shown in the Aspire dashboard

### Benefits of Aspire Orchestration

- **No local SQL Server setup required** - Uses containerized SQL Server
- **Automatic service discovery** - Components find each other automatically
- **Unified logging and monitoring** - All application logs in one place
- **Health checks** - Built-in health monitoring for all services
- **Easy debugging** - Integrated development experience

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test projects
dotnet test tests/VibeCrud.Domain.Tests
dotnet test tests/VibeCrud.Application.Tests
dotnet test tests/VibeCrud.Infrastructure.Tests
```

## Usage

1. **Navigate to Addresses**: Click on "Addresses" in the navigation menu
2. **Add Address**: Click "Add New" to create a new address
3. **Search**: Use the search box to filter addresses
4. **Sort**: Click column headers to sort data
5. **Edit**: Click "Edit" button to modify an address
6. **Delete**: Click "Delete" button to remove an address

## Design Decisions

### Custom Data Grid vs Telerik Kendo Grid
The requirements specified using Telerik Kendo Grid, but since it requires a commercial license, a custom grid component was implemented with equivalent functionality:

**Advantages of Custom Implementation:**
- ✅ No licensing costs
- ✅ Full control over features and performance
- ✅ Optimized for specific use cases
- ✅ Easy to extend and modify

**Feature Parity:**
- ✅ Sorting, filtering, pagination
- ✅ Responsive design
- ✅ Performance optimization for large datasets
- ✅ Professional appearance and UX

### In-Memory Event Bus vs External Message Broker
For simplicity and cost considerations, an in-memory event bus was implemented instead of using external message brokers like RabbitMQ or Azure Service Bus.

**Benefits:**
- ✅ No additional infrastructure required
- ✅ Simple deployment
- ✅ Sufficient for single-instance applications
- ✅ Easy to replace with external broker if needed

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Architecture Benefits

### Maintainability
- Clear separation of concerns
- Testable components
- Loose coupling between layers

### Scalability
- Server-side processing for large datasets
- Efficient database queries with proper indexing
- Async operations throughout

### Extensibility
- New entities can be added easily
- New features can be implemented without affecting existing code
- Event-driven architecture allows for easy integration

### Testability
- Comprehensive test coverage
- Integration tests with real database using Testcontainers
- Mocked dependencies for unit tests