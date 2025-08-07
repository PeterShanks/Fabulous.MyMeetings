CREATE TABLE [UserRegistrations].InternalCommands
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED ,
	[EnqueueDate] DATETIME2(7) NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	[Error] NVARCHAR(MAX) NULL
)
GO

CREATE NONCLUSTERED INDEX IX_Registrations_InternalCommands_Unprocessed
	ON UserRegistrations.InternalCommands(EnqueueDate, Id)
INCLUDE (Type, Data, ProcessedDate, Error)
WHERE ProcessedDate IS NULL
GO