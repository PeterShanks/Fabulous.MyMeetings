CREATE TABLE [Users].[UserRoles]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleCode] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Users_UserRoles_UserId_RoleCode] PRIMARY KEY CLUSTERED ([UserId], [RoleCode]),
    CONSTRAINT [FK_Users_UserRoles_UserId]
        FOREIGN KEY (UserId)
        REFERENCES Users.Users(Id)
)
GO