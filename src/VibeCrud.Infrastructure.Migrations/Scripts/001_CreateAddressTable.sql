-- Create Address table
CREATE TABLE [dbo].[Addresses] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Street] nvarchar(200) NOT NULL,
    [HouseNumber] nvarchar(10) NOT NULL,
    [ZipCode] nvarchar(10) NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Country] nvarchar(100) NOT NULL,
    [Email] nvarchar(200) NULL,
    [Phone] nvarchar(20) NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL DEFAULT 0
);