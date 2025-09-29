CREATE VIEW [Administration].[v_Members]
AS
SELECT
    [Member].[Id],
    [Member].[Email],
    [Member].[FirstName],
    [Member].[LastName],
    [Member].[Name]
FROM [Administration].[Members] AS [Member]
GO