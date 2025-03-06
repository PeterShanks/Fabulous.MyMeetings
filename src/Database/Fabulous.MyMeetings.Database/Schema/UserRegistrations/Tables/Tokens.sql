CREATE TABLE [UserRegistrations].[Tokens]
(
	[ClusterKey] INT NOT NULL IDENTITY(1,1),
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Value] NVARCHAR(255) NOT NULL,
	[TokenTypeId] INT NOT NULL,
	[CreatedAt] DATETIME2(7) NOT NULL,
	[ExpiresAt] DATETIME2(7) NOT NULL,
	[IsUsed] BIT NOT NULL,
	[UsedAt] DATETIME2(7) NULL,
	[IsInvalidated] BIT NOT NULL,
	[InvalidatedAt] DATETIME2(7) NULL,
	CONSTRAINT [PK_UserRegistrations_Tokens_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_UserRegistrations_Tokens_ClusterKey ON UserRegistrations.Tokens(ClusterKey);
GO

ALTER TABLE [UserRegistrations].[Tokens] ADD CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId]
FOREIGN KEY ([TokenTypeId]) REFERENCES [UserRegistrations].[TokenTypes]([Id]);