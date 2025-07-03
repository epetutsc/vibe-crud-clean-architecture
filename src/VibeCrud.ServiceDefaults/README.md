# VibeCrud.ServiceDefaults

This project contains shared service configuration and defaults for .NET Aspire integration across all VibeCrud services.

## Overview

The ServiceDefaults project provides common .NET Aspire services and configurations that are shared across all services in the VibeCrud application:

- **Service Discovery**: Automatic service-to-service communication
- **Health Checks**: Built-in health monitoring endpoints
- **OpenTelemetry**: Distributed tracing, metrics, and logging
- **Observability**: Unified telemetry configuration

## Features

### Service Discovery
Enables automatic discovery and communication between services without hardcoded endpoints.

### Health Checks
Provides standardized health check endpoints:
- `/health` - Overall application health
- `/alive` - Liveness probe for orchestration

### OpenTelemetry Integration
Configures distributed observability with:
- **Tracing**: ASP.NET Core and HTTP client instrumentation
- **Metrics**: Runtime and application performance metrics
- **Logging**: Structured logging with OpenTelemetry formatting

### HTTP Client Configuration
Provides default HTTP client configuration with:
- Service discovery integration
- Standardized timeout and retry policies

## Usage

### In Service Projects

Add reference to this project and call the extension method in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components
builder.AddServiceDefaults();

// ... other service configuration

var app = builder.Build();

// Map default endpoints (health checks, etc.)
app.MapDefaultEndpoints();

// ... other middleware configuration

app.Run();
```

## Dependencies

- **Microsoft.Extensions.ServiceDiscovery**: Service discovery capabilities
- **OpenTelemetry packages**: Observability and telemetry
- **Microsoft.Extensions.Http.Resilience**: HTTP client resilience patterns

## Configuration

### OpenTelemetry Exporter

The OpenTelemetry exporter is configured automatically when the `OTEL_EXPORTER_OTLP_ENDPOINT` environment variable is set. This is typically handled by the Aspire orchestration.

### Health Check Configuration

Health checks are automatically registered with:
- A self-check that always returns healthy
- Tagged with "live" for liveness probes

## Integration with Aspire

This project is designed to work seamlessly with .NET Aspire:
- Services using these defaults are automatically discoverable
- Health checks integrate with Aspire dashboard monitoring
- Telemetry data flows to the Aspire observability stack

## Development vs Production

### Development
- Health check endpoints are exposed for debugging
- Detailed telemetry collection enabled
- Service discovery points to local orchestrated services

### Production
- Health check endpoints may be restricted for security
- Telemetry can be configured to external systems
- Service discovery uses production service endpoints