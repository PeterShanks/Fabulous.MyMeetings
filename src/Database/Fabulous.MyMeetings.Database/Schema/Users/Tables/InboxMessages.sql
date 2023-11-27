CREATE TABLE [Users].InboxMessages
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Users_InboxMessages_Id] PRIMARY KEY NONCLUSTERED([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_InboxMessages_OccurredOn_ClusterKey ON Users.InboxMessages(OccurredOn, ClusterKey);
GO

CREATE NONCLUSTERED INDEX IX_Users_InboxMessages_ProcessedDate
	ON Users.InboxMessages(ProcessedDate)
	WHERE ProcessedDate IS NULL