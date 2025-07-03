using Moq;
using VibeCrud.Application.DTOs;
using VibeCrud.Application.Services;
using VibeCrud.Domain.Entities;
using VibeCrud.Domain.Interfaces;

namespace VibeCrud.Application.Tests;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _mockRepository;
    private readonly Mock<IEventBus> _mockEventBus;
    private readonly AddressService _addressService;

    public AddressServiceTests()
    {
        _mockRepository = new Mock<IAddressRepository>();
        _mockEventBus = new Mock<IEventBus>();
        _addressService = new AddressService(_mockRepository.Object, _mockEventBus.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_AddressDto_When_Address_Exists()
    {
        // Arrange
        var address = new Address
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Street = "Main Street",
            HouseNumber = "123",
            ZipCode = "12345",
            City = "New York",
            Country = "USA",
            Email = "john.doe@example.com",
            Phone = "+1234567890",
            CreatedAt = DateTime.UtcNow
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(address);

        // Act
        var result = await _addressService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("John Doe", result.FullName);
        Assert.Equal("Main Street 123, 12345 New York, USA", result.FullAddress);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Address_Not_Found()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Address?)null);

        // Act
        var result = await _addressService.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Address_And_Publish_Event()
    {
        // Arrange
        var createDto = new CreateAddressDto
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

        var createdAddress = new Address
        {
            Id = 1,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Street = createDto.Street,
            HouseNumber = createDto.HouseNumber,
            ZipCode = createDto.ZipCode,
            City = createDto.City,
            Country = createDto.Country,
            Email = createDto.Email,
            Phone = createDto.Phone,
            CreatedAt = DateTime.UtcNow
        };

        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Address>()))
            .ReturnsAsync(createdAddress);

        // Act
        var result = await _addressService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);

        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Address>()), Times.Once);
        _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<AddressCreatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task GetPagedAsync_Should_Return_Paged_Results()
    {
        // Arrange
        var addresses = new List<Address>
        {
            new Address { Id = 1, FirstName = "John", LastName = "Doe" },
            new Address { Id = 2, FirstName = "Jane", LastName = "Smith" }
        };

        _mockRepository.Setup(r => r.GetPagedAsync(1, 10, null, null, null))
            .ReturnsAsync((addresses, 2));

        var query = new AddressQueryDto { Page = 1, PageSize = 10 };

        // Act
        var result = await _addressService.GetPagedAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(2, result.Items.Count());
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Address_And_Publish_Event()
    {
        // Arrange
        var existingAddress = new Address
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Street = "Main Street",
            HouseNumber = "123",
            ZipCode = "12345",
            City = "New York",
            Country = "USA",
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };

        var updateDto = new UpdateAddressDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Street = "Updated Street",
            HouseNumber = "456",
            ZipCode = "54321",
            City = "Boston",
            Country = "USA"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existingAddress);

        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Address>()))
            .ReturnsAsync(existingAddress);

        // Act
        var result = await _addressService.UpdateAsync(updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Street", result.Street);
        Assert.Equal("456", result.HouseNumber);

        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Address>()), Times.Once);
        _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<AddressUpdatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Address_And_Publish_Event()
    {
        // Arrange
        var existingAddress = new Address
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existingAddress);

        // Act
        await _addressService.DeleteAsync(1);

        // Assert
        _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        _mockEventBus.Verify(e => e.PublishAsync(It.IsAny<AddressDeletedEvent>()), Times.Once);
    }
}