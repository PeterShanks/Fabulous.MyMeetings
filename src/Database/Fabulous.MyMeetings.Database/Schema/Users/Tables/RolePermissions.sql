CREATE TABLE [Users].[RolesToPermissions]
(
	[ClusterKey] INT IDENTITY(1, 1) NOT NULL,
	[RoleCode] VARCHAR(50) NOT NULL,
	[PermissionCode] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_RolesToPermissions_RoleCode_PermissionCode] PRIMARY KEY NONCLUSTERED (RoleCode ASC, PermissionCode ASC)
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_RolesToPermissions_ClusterKey ON Users.RolesToPermissions(ClusterKey);