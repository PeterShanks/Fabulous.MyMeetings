CREATE TABLE [Users].[RolePermissions]
(
	[RoleCode] VARCHAR(50) NOT NULL,
	[PermissionCode] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_RolePermissions_RoleCode_PermissionCode] PRIMARY KEY CLUSTERED (RoleCode ASC, PermissionCode ASC),
	CONSTRAINT [FK_Users_RolePermissions_PermissionCode]
		FOREIGN KEY ([PermissionCode])
		REFERENCES Users.Permissions(Code)
)
GO