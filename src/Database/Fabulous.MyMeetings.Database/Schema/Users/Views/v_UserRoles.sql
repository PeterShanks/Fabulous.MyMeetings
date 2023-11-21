CREATE VIEW [Users].[v_UserRoles]
AS
SELECT
    [UserRole].[UserId],
    [UserRole].[RoleCode]
FROM [Users].[UserRoles] AS [UserRole]
GO