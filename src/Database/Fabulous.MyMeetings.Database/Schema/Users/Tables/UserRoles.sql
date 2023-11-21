CREATE TABLE [Users].[UserRoles]
(
    [ClusterKey] INT IDENTITY(1, 1) NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleCode] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Users_UserRoles_UserId_RoleCode] PRIMARY KEY NONCLUSTERED ([UserId], [RoleCode]),
)
GO

CREATE UNIQUE CLUSTERED INDEX IX_Users_UserRoles_ClusterKey ON Users.UserRoles(ClusterKey);