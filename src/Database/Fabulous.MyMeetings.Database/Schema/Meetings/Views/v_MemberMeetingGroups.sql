CREATE VIEW [Meetings].[v_MemberMeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity],

    [MeetingGroupMember].[MemberId],
    [MeetingGroupMember].[RoleCode],
    [MeetingGroupMember].[IsActive],
    [MeetingGroupMember].[JoinedDate]
FROM meetings.MeetingGroups AS [MeetingGroup]
    INNER JOIN [Meetings].[MeetingGroupMembers] AS [MeetingGroupMember]
        ON [MeetingGroup].[Id] = [MeetingGroupMember].[MeetingGroupId]
GO