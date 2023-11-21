CREATE TABLE [Users].[UserRegistrations]
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Login] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR (255) NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR (255) NOT NULL,
	[StatusCode] VARCHAR(50) NOT NULL,
	[RegisterDate] DATETIME2(7) NOT NULL,
	[ConfirmedDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Users_UserRegistrations_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_UserRegistrations_ClusterKey ON Users.UserRegistrations(ClusterKey);