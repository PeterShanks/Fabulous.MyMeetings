CREATE TABLE [Users].[Permissions]
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[Code] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[Description] [varchar](255) NULL,
	CONSTRAINT [PK_Users_Permissions_Code] PRIMARY KEY NONCLUSTERED ([Code] ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_Permissions_ClusterKey ON Users.Permissions(ClusterKey);
