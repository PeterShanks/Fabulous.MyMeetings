CREATE TABLE [Users].OutboxMessages
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Users_OutboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_OutboxMessages_OccurredOn_ClusterKey ON Users.OutboxMessages(OccurredOn,ClusterKey);
GO

CREATE NONCLUSTERED INDEX IX_Users_OutboxMessages_ProcessedDate
	ON Users.OutboxMessages(ProcessedDate)
	WHERE ProcessedDate IS NULL