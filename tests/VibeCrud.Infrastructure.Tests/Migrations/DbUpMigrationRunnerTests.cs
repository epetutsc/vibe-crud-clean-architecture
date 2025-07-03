using Microsoft.Extensions.Logging;
using Moq;
using VibeCrud.Infrastructure.Migrations;
using Xunit;

namespace VibeCrud.Infrastructure.Tests.Migrations;

public class DbUpMigrationRunnerTests
{
    [Fact]
    public void Constructor_ShouldNotThrow()
    {
        // Arrange
        var logger = new Mock<ILogger<DbUpMigrationRunner>>();

        // Act & Assert
        var runner = new DbUpMigrationRunner(logger.Object);
        Assert.NotNull(runner);
    }

    [Fact]
    public async Task RunMigrationsAsync_WithInvalidConnectionString_ShouldReturnFalse()
    {
        // Arrange
        var logger = new Mock<ILogger<DbUpMigrationRunner>>();
        var runner = new DbUpMigrationRunner(logger.Object);
        var invalidConnectionString = "invalid connection string";

        // Act
        var result = await runner.RunMigrationsAsync(invalidConnectionString);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RunMigrationsAsync_WithEmptyConnectionString_ShouldReturnFalse()
    {
        // Arrange
        var logger = new Mock<ILogger<DbUpMigrationRunner>>();
        var runner = new DbUpMigrationRunner(logger.Object);

        // Act
        var result = await runner.RunMigrationsAsync(string.Empty);

        // Assert
        Assert.False(result);
    }
}