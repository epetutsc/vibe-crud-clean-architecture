# VibeCrud.Web

This project contains the Blazor Server web application for the VibeCrud address management system.

## Overview

The Web layer provides the user interface and handles HTTP requests. It's built using Blazor Server, which provides a rich, interactive web application with real-time updates.

## Structure

### Components
- **Pages**: Blazor pages that represent different views
  - `Addresses.razor`: Main address management page with CRUD operations
- **Common Components**: Reusable UI components
  - `AddressGrid.razor`: Custom data grid with sorting, filtering, and pagination
- **Layout**: Application layout components
  - `MainLayout.razor`: Main application layout
  - `NavMenu.razor`: Navigation menu

### Configuration
- `Program.cs`: Application startup and service configuration
- `appsettings.json`: Application configuration including connection strings

## Key Features

### Address Management Interface
The main features include:
- **Data Grid**: Custom implementation with advanced features
  - Sorting by any column (ascending/descending)
  - Global text filtering across all fields
  - Pagination with configurable page sizes (10, 25, 50, 100)
  - Responsive design for mobile devices
- **CRUD Operations**: 
  - Create new addresses with modal dialog
  - Edit existing addresses
  - Delete addresses with confirmation
  - View address details

### Custom Data Grid Component
The `AddressGrid` component provides Telerik Kendo Grid-like functionality:
- **Performance Optimized**: Server-side processing for large datasets
- **Sorting**: Click column headers to sort data
- **Filtering**: Real-time search across all address fields
- **Pagination**: Navigate through large datasets efficiently
- **Responsive**: Mobile-friendly design with Bootstrap CSS

### User Experience Features
- **Interactive UI**: Real-time updates without page refreshes
- **Loading States**: Visual feedback during operations
- **Error Handling**: User-friendly error messages
- **Confirmation Dialogs**: Prevent accidental deletions
- **Form Validation**: Client-side validation for data entry

## Dependencies

- **Microsoft.AspNetCore.Components**: Blazor Server framework
- **VibeCrud.Application**: Application services and DTOs
- **VibeCrud.Infrastructure**: Infrastructure implementations
- **Bootstrap**: CSS framework for responsive design

## Configuration

### Database Connection
The application uses SQL Server with the connection string configured in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VibeCrudDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Service Registration
Services are registered in `Program.cs`:
```csharp
// Infrastructure services (Database, Repositories, Event Bus)
builder.Services.AddInfrastructure(builder.Configuration);

// Application services
builder.Services.AddScoped<IAddressService, AddressService>();
```

## Custom Grid Implementation

Since Telerik Kendo Grid requires a commercial license, a custom grid component was implemented with equivalent functionality:

### Features Parity
- ✅ Sorting (all columns, ascending/descending)
- ✅ Filtering (global search across all fields)
- ✅ Pagination (configurable page sizes)
- ✅ Responsive design
- ✅ Loading states
- ✅ CRUD operations
- ✅ Performance optimized for large datasets

### Performance Optimization
- **Server-side Processing**: Only requested data is transferred
- **Lazy Loading**: Data is loaded on-demand
- **Efficient Queries**: Database queries are optimized with indexes
- **Minimal DOM Updates**: Blazor's efficient rendering

## Responsive Design

The application is fully responsive and works on:
- **Desktop**: Full feature set with optimal layout
- **Tablet**: Adapted layout with touch-friendly controls
- **Mobile**: Compact layout with essential features

## Testing

The web application is tested through:
- **Integration Tests**: Test the complete request pipeline
- **Component Tests**: Test individual Blazor components
- **E2E Tests**: End-to-end user journey testing

See `VibeCrud.Web.Tests` for test examples.

## Running the Application

1. Ensure SQL Server or LocalDB is available
2. Run database migrations (if needed)
3. Start the application: `dotnet run`
4. Navigate to the Addresses page to manage address data

The application will automatically create the database if it doesn't exist.