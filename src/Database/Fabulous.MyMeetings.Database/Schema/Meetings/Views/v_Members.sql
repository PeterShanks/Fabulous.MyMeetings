CREATE VIEW [Meetings].[v_Members]
AS
SELECT
    [Member].Id,
    [Member].[Name],
    [Member].[Email]
FROM [Meetings].Members AS [Member]
GO