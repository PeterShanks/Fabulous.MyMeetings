CREATE VIEW [Meetings].[v_Countries]
AS
SELECT
    [Country].[Code],
    [Country].[Name]
FROM Meetings.Countries AS [Country]
GO