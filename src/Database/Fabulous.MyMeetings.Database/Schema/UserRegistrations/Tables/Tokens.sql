CREATE TABLE [UserRegistrations].[Tokens]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Value] NVARCHAR(255) NOT NULL,
	[TokenTypeId] INT NOT NULL,
	[CreatedAt] DATETIME2(7) NOT NULL,
	[ExpiresAt] DATETIME2(7) NOT NULL,
	[IsUsed] BIT NOT NULL,
	[UsedAt] DATETIME2(7) NULL,
	[IsInvalidated] BIT NOT NULL,
	[InvalidatedAt] DATETIME2(7) NULL
)
GO

ALTER TABLE [UserRegistrations].[Tokens] ADD CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId]
FOREIGN KEY ([TokenTypeId]) REFERENCES [UserRegistrations].[TokenTypes]([Id]);
GO