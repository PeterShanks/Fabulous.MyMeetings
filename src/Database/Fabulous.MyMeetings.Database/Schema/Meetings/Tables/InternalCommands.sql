CREATE TABLE [Meetings].[InternalCommands]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
	[EnqueueDate] DATETIME2(7) NOT NULL,
	[Type] NVARCHAR(255) NOT NULL,
	[Data] NVARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	[Error] NVARCHAR(MAX) NULL
)
GO

CREATE NONCLUSTERED INDEX IX_Meetings_InternalCommands_Unprocessed
	ON Meetings.InternalCommands(EnqueueDate, Id)
	INCLUDE (Type, Data, ProcessedDate, Error)
	WHERE ProcessedDate IS NULL
