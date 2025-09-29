CREATE TABLE [Administration].[InboxMessages]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
	[OccurredOn] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL
)
GO

CREATE NONCLUSTERED INDEX IX_Administration_InboxMessages_Unprocessed
	ON Administration.InboxMessages(OccurredOn, Id)
	INCLUDE (Type, Data, ProcessedDate)
	WHERE ProcessedDate IS NULL