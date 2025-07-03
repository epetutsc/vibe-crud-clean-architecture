# VibeCrud.AppHost

This project serves as the .NET Aspire orchestration host for the VibeCrud application, providing simplified local development experience.

## Overview

The AppHost project configures and orchestrates all the components needed to run the VibeCrud application locally:

- **SQL Server**: Runs in a Docker container with persistent data
- **VibeCrud.Web**: The main Blazor Server web application
- **Service Discovery**: Automatic service-to-service communication
- **Health Monitoring**: Built-in health checks for all services
- **Observability**: Integrated logging, metrics, and tracing

## Usage

### Starting the Application

```bash
# From the repository root
dotnet run --project src/VibeCrud.AppHost
```

This will:
1. Start the Aspire dashboard at `http://localhost:15000`
2. Launch SQL Server in a Docker container
3. Start the web application with automatic service discovery
4. Configure health checks and monitoring

### Aspire Dashboard

The dashboard provides:
- **Resource Overview**: Status of all orchestrated services
- **Logs**: Unified view of application logs
- **Metrics**: Performance and health metrics
- **Traces**: Distributed tracing information
- **Environment Variables**: Service configuration

## Configuration

### SQL Server

The SQL Server is configured with:
- **Container**: Uses the official SQL Server Docker image
- **Data Persistence**: Data volume mounted for persistence
- **Database**: Automatically creates the `VibeCrudDb` database
- **Connection String**: Automatically provided to the web application

### Web Application

The web application is configured with:
- **Service Discovery**: Automatic discovery of the SQL Server
- **Health Checks**: Built-in health monitoring endpoints
- **Observability**: OpenTelemetry integration for logging and tracing

## Development Benefits

### Simplified Setup
- No need to install SQL Server locally
- Automatic container management
- Zero-configuration service discovery

### Enhanced Debugging
- Centralized logging across all services
- Real-time health monitoring
- Distributed tracing for request flows

### Production-like Environment
- Containerized dependencies
- Service isolation
- Realistic networking scenarios

## Prerequisites

- **.NET 8 SDK**
- **Docker Desktop** (for SQL Server container)
- **.NET Aspire workload**: `dotnet workload install aspire`

## Troubleshooting

### Docker Issues
If you encounter Docker-related errors:
1. Ensure Docker Desktop is running
2. Verify Docker has sufficient resources allocated
3. Check that port 1433 is not already in use

### Port Conflicts
The dashboard runs on port 15000 by default. If this conflicts:
1. Modify the `applicationUrl` in `Properties/launchSettings.json`
2. Or set the `ASPNETCORE_URLS` environment variable

### Service Discovery Issues
If services can't find each other:
1. Check the Aspire dashboard for service status
2. Verify all services are running (green status)
3. Look at the logs for connection errors