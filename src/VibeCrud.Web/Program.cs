using VibeCrud.Web.Components;
using VibeCrud.Infrastructure;
using VibeCrud.Application.Services;
using VibeCrud.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application services
builder.Services.AddScoped<IAddressService, AddressService>();

var app = builder.Build();

// Run database migrations
using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    if (!string.IsNullOrEmpty(connectionString))
    {
        var migrationSuccess = await migrationRunner.RunMigrationsAsync(connectionString);
        if (!migrationSuccess)
        {
            app.Logger.LogError("Database migrations failed. Application may not function correctly.");
        }
    }
    else
    {
        app.Logger.LogWarning("No connection string found. Skipping database migrations.");
    }
}

// Map default endpoints (health checks, etc.)
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

// Make the Program class public for testing
public partial class Program { }
