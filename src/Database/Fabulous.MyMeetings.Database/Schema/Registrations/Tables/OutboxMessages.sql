CREATE TABLE [Registrations].OutboxMessages
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Registrations_OutboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Registrations_OutboxMessages_OccurredOn_ClusterKey ON Registrations.OutboxMessages(OccurredOn,ClusterKey);
GO

CREATE NONCLUSTERED INDEX IX_Registrations_OutboxMessages_ProcessedDate
	ON Registrations.OutboxMessages(ProcessedDate)
	WHERE ProcessedDate IS NULL