# VibeCrud.Infrastructure.Migrations

This project handles database migrations using DbUp instead of Entity Framework Core migrations.

## Overview

This infrastructure project is responsible for:
- Database schema creation and updates
- SQL script management
- Migration execution
- Database version tracking

## Key Components

### DbUpMigrationRunner
- Implements `IMigrationRunner` interface
- Handles execution of SQL migration scripts
- Provides logging and error handling
- Uses embedded resources for SQL scripts

### Migration Scripts
Located in the `Scripts` folder, SQL migration scripts are:
- Named with incrementing numbers (001_, 002_, etc.)
- Embedded as resources in the assembly
- Executed in order by DbUp
- Tracked to prevent re-execution

## Usage

### Dependency Injection
```csharp
services.AddMigrationsInfrastructure();
```

### Running Migrations
```csharp
var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
var success = await migrationRunner.RunMigrationsAsync(connectionString);
```

## Migration Scripts

### 001_CreateAddressTable.sql
Creates the main Address table with:
- GUID primary key
- Required fields (FirstName, LastName, Street, etc.)
- Optional fields (Email, Phone)
- Audit fields (CreatedAt, UpdatedAt)
- Soft delete support (IsDeleted)

### 002_CreateAddressIndexes.sql
Creates performance indexes:
- Unique index on Email (with NULL filter)
- Composite index on FirstName + LastName
- Single indexes on City, ZipCode, IsDeleted

## Dependencies

- **DbUp-SqlServer**: SQL Server migration engine
- **Microsoft.Extensions.DependencyInjection**: Dependency injection abstractions
- **Microsoft.Extensions.Logging**: Logging abstractions

## Design Principles

- **Single Responsibility**: Focused only on database migrations
- **Clean Architecture**: No dependencies on other application layers
- **Testability**: Interface-based design allows for easy mocking
- **Performance**: Strategic indexing for optimal query performance