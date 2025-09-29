CREATE TABLE [UserRegistrations].OutboxMessages
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
	[OccurredOn] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL
)
GO

CREATE NONCLUSTERED INDEX IX_Registrations_OutboxMessages_Unprocessed
	ON UserRegistrations.OutboxMessages(OccurredOn, Id)
	INCLUDE (Type, Data, ProcessedDate)
	WHERE ProcessedDate IS NULL