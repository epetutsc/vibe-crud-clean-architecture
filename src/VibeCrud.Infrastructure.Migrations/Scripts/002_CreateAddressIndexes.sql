-- Create indexes for Address table performance optimization
CREATE UNIQUE NONCLUSTERED INDEX [IX_Addresses_Email] 
ON [dbo].[Addresses] ([Email])
WHERE [Email] IS NOT NULL;

CREATE NONCLUSTERED INDEX [IX_Addresses_FirstName_LastName] 
ON [dbo].[Addresses] ([FirstName], [LastName]);

CREATE NONCLUSTERED INDEX [IX_Addresses_City] 
ON [dbo].[Addresses] ([City]);

CREATE NONCLUSTERED INDEX [IX_Addresses_ZipCode] 
ON [dbo].[Addresses] ([ZipCode]);

CREATE NONCLUSTERED INDEX [IX_Addresses_IsDeleted] 
ON [dbo].[Addresses] ([IsDeleted]);