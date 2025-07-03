# VibeCrud.Infrastructure.SqlServer

This project contains SQL Server-specific infrastructure implementations for the VibeCrud application.

## Purpose

This project provides concrete implementations for data persistence using Entity Framework Core and SQL Server. It is separated from other infrastructure concerns to maintain a modular architecture.

## Contents

### Data Access
- **VibeCrudDbContext**: Entity Framework Core database context with SQL Server configuration
- **Entity Configurations**: Entity mappings and database schema configuration

### Repositories
- **AddressRepository**: SQL Server implementation of the address repository interface
- **Advanced Querying**: Server-side filtering, sorting, and pagination support

### Database Features
- Strategic database indexes for performance optimization
- Soft delete implementation
- Audit trail support (CreatedAt/UpdatedAt timestamps)
- Entity Framework migrations support

## Dependencies

- **VibeCrud.Domain**: References domain entities and interfaces
- **Entity Framework Core**: Data access framework
- **SQL Server**: Database provider
- **Microsoft.Extensions.DependencyInjection**: Dependency injection support

## Configuration

Use the `AddSqlServerInfrastructure` extension method to register all SQL Server-related services:

```csharp
services.AddSqlServerInfrastructure(configuration);
```

This registers:
- Entity Framework DbContext with SQL Server provider
- Address repository implementation
- Database connection configuration

## Database Schema

The project includes Entity Framework configurations for:
- Address entity with proper constraints and indexes
- Audit fields (CreatedAt, UpdatedAt)
- Soft delete support (IsDeleted flag)
- Performance optimization indexes

## Migration Support

This project contains the Entity Framework migrations assembly configuration for maintaining database schema changes over time.