using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using VibeCrud.Application.Services;

namespace VibeCrud.Web.Tests;

public class WebApplicationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WebApplicationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Homepage_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task Get_AddressesPage_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/addresses");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public void Services_Should_Be_Registered()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var services = scope.ServiceProvider;

        // Act & Assert
        var addressService = services.GetService<IAddressService>();
        Assert.NotNull(addressService);
    }
}