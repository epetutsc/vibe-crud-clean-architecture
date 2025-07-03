using VibeCrud.Domain.Entities;

namespace VibeCrud.Domain.Tests;

public class AddressTests
{
    [Fact]
    public void Address_Should_Calculate_FullName_Correctly()
    {
        // Arrange
        var address = new Address
        {
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var fullName = address.FullName;

        // Assert
        Assert.Equal("John Doe", fullName);
    }

    [Fact]
    public void Address_Should_Calculate_FullAddress_Correctly()
    {
        // Arrange
        var address = new Address
        {
            Street = "Main Street",
            HouseNumber = "123",
            ZipCode = "12345",
            City = "New York",
            Country = "USA"
        };

        // Act
        var fullAddress = address.FullAddress;

        // Assert
        Assert.Equal("Main Street 123, 12345 New York, USA", fullAddress);
    }

    [Fact]
    public void Address_Should_Initialize_With_Default_Values()
    {
        // Arrange & Act
        var address = new Address();

        // Assert
        Assert.Equal(0, address.Id);
        Assert.Equal(string.Empty, address.FirstName);
        Assert.Equal(string.Empty, address.LastName);
        Assert.Equal(string.Empty, address.Street);
        Assert.Equal(string.Empty, address.HouseNumber);
        Assert.Equal(string.Empty, address.ZipCode);
        Assert.Equal(string.Empty, address.City);
        Assert.Equal(string.Empty, address.Country);
        Assert.Null(address.Email);
        Assert.Null(address.Phone);
        Assert.False(address.IsDeleted);
    }
}