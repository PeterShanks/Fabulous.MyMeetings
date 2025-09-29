CREATE TABLE [Users].[Permissions]
(
	[Code] VARCHAR(50) NOT NULL PRIMARY KEY CLUSTERED,
	[Name] VARCHAR(100) NOT NULL,
	[Description] [varchar](255) NULL
)
GO