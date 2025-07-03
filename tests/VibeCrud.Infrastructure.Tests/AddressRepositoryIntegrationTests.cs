using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using VibeCrud.Domain.Entities;
using VibeCrud.Infrastructure.SqlServer.Data;
using VibeCrud.Infrastructure.SqlServer.Repositories;

namespace VibeCrud.Infrastructure.Tests;

public class AddressRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _container;
    private VibeCrudDbContext _context = null!;
    private AddressRepository _repository = null!;

    public AddressRepositoryIntegrationTests()
    {
        _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var connectionString = _container.GetConnectionString();
        var options = new DbContextOptionsBuilder<VibeCrudDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _context = new VibeCrudDbContext(options);
        await _context.Database.EnsureCreatedAsync();

        _repository = new AddressRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _container.DisposeAsync();
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Address_In_Database()
    {
        // Arrange
        var address = new Address
        {
            FirstName = "John",
            LastName = "Doe",
            Street = "Main Street",
            HouseNumber = "123",
            ZipCode = "12345",
            City = "New York",
            Country = "USA",
            Email = "john.doe@example.com",
            Phone = "+1234567890"
        };

        // Act
        var result = await _repository.CreateAsync(address);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.True(result.CreatedAt > DateTime.MinValue);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Address_When_Exists()
    {
        // Arrange
        var address = new Address
        {
            FirstName = "Jane",
            LastName = "Smith",
            Street = "Second Street",
            HouseNumber = "456",
            ZipCode = "54321",
            City = "Boston",
            Country = "USA"
        };

        var created = await _repository.CreateAsync(address);

        // Act
        var result = await _repository.GetByIdAsync(created.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Smith", result.LastName);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Address_In_Database()
    {
        // Arrange
        var address = new Address
        {
            FirstName = "Bob",
            LastName = "Johnson",
            Street = "Third Street",
            HouseNumber = "789",
            ZipCode = "67890",
            City = "Chicago",
            Country = "USA"
        };

        var created = await _repository.CreateAsync(address);
        created.FirstName = "Robert";
        created.City = "Los Angeles";

        // Act
        var result = await _repository.UpdateAsync(created);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Robert", result.FirstName);
        Assert.Equal("Los Angeles", result.City);
        Assert.NotNull(result.UpdatedAt);
    }

    [Fact]
    public async Task DeleteAsync_Should_Soft_Delete_Address()
    {
        // Arrange
        var address = new Address
        {
            FirstName = "Alice",
            LastName = "Brown",
            Street = "Fourth Street",
            HouseNumber = "101",
            ZipCode = "11111",
            City = "Miami",
            Country = "USA"
        };

        var created = await _repository.CreateAsync(address);

        // Act
        await _repository.DeleteAsync(created.Id);

        // Assert
        var result = await _repository.GetByIdAsync(created.Id);
        Assert.Null(result); // Should not be found after soft delete
    }

    [Fact]
    public async Task GetPagedAsync_Should_Return_Paged_Results()
    {
        // Arrange
        var addresses = new List<Address>
        {
            new Address { FirstName = "Test1", LastName = "User1", Street = "Street1", HouseNumber = "1", ZipCode = "11111", City = "City1", Country = "Country1" },
            new Address { FirstName = "Test2", LastName = "User2", Street = "Street2", HouseNumber = "2", ZipCode = "22222", City = "City2", Country = "Country2" },
            new Address { FirstName = "Test3", LastName = "User3", Street = "Street3", HouseNumber = "3", ZipCode = "33333", City = "City3", Country = "Country3" }
        };

        foreach (var addr in addresses)
        {
            await _repository.CreateAsync(addr);
        }

        // Act
        var result = await _repository.GetPagedAsync(1, 2);

        // Assert
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(3, result.TotalCount);
    }

    [Fact]
    public async Task GetPagedAsync_Should_Filter_Results()
    {
        // Arrange
        var addresses = new List<Address>
        {
            new Address { FirstName = "John", LastName = "Doe", Street = "Main Street", HouseNumber = "1", ZipCode = "11111", City = "New York", Country = "USA" },
            new Address { FirstName = "Jane", LastName = "Smith", Street = "Second Street", HouseNumber = "2", ZipCode = "22222", City = "Boston", Country = "USA" }
        };

        foreach (var addr in addresses)
        {
            await _repository.CreateAsync(addr);
        }

        // Act
        var result = await _repository.GetPagedAsync(1, 10, filter: "John");

        // Assert
        Assert.Single(result.Items);
        Assert.Equal("John", result.Items.First().FirstName);
    }

    [Fact]
    public async Task GetPagedAsync_Should_Sort_Results()
    {
        // Arrange
        var addresses = new List<Address>
        {
            new Address { FirstName = "Charlie", LastName = "Brown", Street = "Street1", HouseNumber = "1", ZipCode = "11111", City = "City1", Country = "Country1" },
            new Address { FirstName = "Alice", LastName = "Smith", Street = "Street2", HouseNumber = "2", ZipCode = "22222", City = "City2", Country = "Country2" },
            new Address { FirstName = "Bob", LastName = "Johnson", Street = "Street3", HouseNumber = "3", ZipCode = "33333", City = "City3", Country = "Country3" }
        };

        foreach (var addr in addresses)
        {
            await _repository.CreateAsync(addr);
        }

        // Act
        var result = await _repository.GetPagedAsync(1, 10, sortBy: "FirstName", sortDirection: "asc");

        // Assert
        Assert.Equal(3, result.Items.Count());
        Assert.Equal("Alice", result.Items.First().FirstName);
        Assert.Equal("Charlie", result.Items.Last().FirstName);
    }
}