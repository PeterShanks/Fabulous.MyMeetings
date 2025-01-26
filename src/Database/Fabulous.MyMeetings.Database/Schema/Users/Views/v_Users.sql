CREATE VIEW [Users].[v_Users]
AS
SELECT
    [User].[Id],
    [User].[IsActive],
    [User].[Password],
    [User].[Email],
    [User].[Name]
FROM [Users].[Users] AS [User]
GO