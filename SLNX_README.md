# Solution File Conversion to SLNX Format

This repository now includes a `VibeCrud.slnx` file, which is the XML-based successor to the traditional `.sln` format.

## What is SLNX?

The `.slnx` format is Microsoft's new XML-based solution file format that provides:
- Better readability and editability
- Improved merge conflict resolution in version control
- More structured representation of solution hierarchy
- Future-proof format for modern .NET development

## File Structure

The `VibeCrud.slnx` contains the same project structure as `VibeCrud.sln`:

### Source Projects (src folder):
- VibeCrud.AppHost
- VibeCrud.Domain  
- VibeCrud.Application
- VibeCrud.Infrastructure
- VibeCrud.Infrastructure.SqlServer
- VibeCrud.Infrastructure.Messaging
- VibeCrud.Infrastructure.Migrations
- VibeCrud.ServiceDefaults
- VibeCrud.Web

### Test Projects (tests folder):
- VibeCrud.Domain.Tests
- VibeCrud.Application.Tests
- VibeCrud.Infrastructure.Tests
- VibeCrud.Web.Tests

## Compatibility

The SLNX format requires newer versions of .NET SDK and Visual Studio. Current compatibility:
- .NET 8.0+ SDK (some versions may have limited support)
- Visual Studio 2022 17.8+ (with proper extensions)
- JetBrains Rider 2023.3+

## Usage

Once your development environment supports SLNX format, you can use it just like a traditional solution file:

```bash
dotnet build VibeCrud.slnx
dotnet test VibeCrud.slnx
```

## Migration Notes

- The original `VibeCrud.sln` file is preserved for backward compatibility
- Both files represent the same solution structure
- Choose the appropriate file based on your tooling support