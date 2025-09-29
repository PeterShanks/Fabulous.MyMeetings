CREATE VIEW [Meetings].[v_MeetingAttendees]
AS
SELECT
    [MeetingAttendee].[MeetingId],
    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[DecisionDate],
    [MeetingAttendee].[RoleCode],
    [MeetingAttendee].[GuestsNumber],

    [Member].[FirstName],
    [Member].[LastName]
FROM [Meetings].[MeetingAttendees] AS [MeetingAttendee]
    INNER JOIN [Meetings].[Members] AS [Member]
        ON [MeetingAttendee].[AttendeeId] = [Member].[Id]
GO