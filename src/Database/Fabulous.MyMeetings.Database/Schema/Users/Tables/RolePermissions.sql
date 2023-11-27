CREATE TABLE [Users].[RolePermissions]
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[RoleCode] VARCHAR(50) NOT NULL,
	[PermissionCode] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_RolePermissions_RoleCode_PermissionCode] PRIMARY KEY NONCLUSTERED (RoleCode ASC, PermissionCode ASC),
	CONSTRAINT [FK_Users_RolePermissions_PermissionCode]
		FOREIGN KEY ([PermissionCode])
		REFERENCES Users.Permissions(Code)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_RolePermissions_ClusterKey ON Users.RolePermissions(ClusterKey);