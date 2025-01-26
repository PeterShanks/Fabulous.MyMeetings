CREATE TABLE [UserRegistrations].InternalCommands
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EnqueueDate] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2(7) NULL,
	[Error] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_Registrations_InternalCommands_Id] PRIMARY KEY NONCLUSTERED([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Registrations_InternalCommands_EnqueueDate_ClusterKey ON UserRegistrations.InternalCommands(EnqueueDate, ClusterKey);
GO

CREATE NONCLUSTERED INDEX IX_Registrations_InternalCommands_ProcessedDate
	ON UserRegistrations.InternalCommands(ProcessedDate)
	WHERE ProcessedDate IS NULL