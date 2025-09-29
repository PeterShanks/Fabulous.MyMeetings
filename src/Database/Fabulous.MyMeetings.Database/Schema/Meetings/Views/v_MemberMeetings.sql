CREATE VIEW [Meetings].[v_MemberMeetings]
AS
SELECT
	[Meeting].[Id],
    [Meeting].[Title],
    [Meeting].[Description],
    [Meeting].[LocationAddress],
    [Meeting].[LocationCity],
    [Meeting].[LocationPostalCode],
    [Meeting].[TermStartDate],
    [Meeting].[TermEndDate],

    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[IsRemoved],
    [MeetingAttendee].[RoleCode]
FROM [Meetings].[Meetings] AS [Meeting]
    INNER JOIN [Meetings].[MeetingAttendees] AS [MeetingAttendee]
        ON [Meeting].[Id] = [MeetingAttendee].[MeetingId]
GO