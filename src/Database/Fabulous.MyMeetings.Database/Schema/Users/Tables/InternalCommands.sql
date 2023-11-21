CREATE TABLE [Users].InternalCommands
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EnqueueDate] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	[Error] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_Users_InternalCommands_Id] PRIMARY KEY NONCLUSTERED([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_InternalCommands_ClusterKey ON Users.InternalCommands(ClusterKey);