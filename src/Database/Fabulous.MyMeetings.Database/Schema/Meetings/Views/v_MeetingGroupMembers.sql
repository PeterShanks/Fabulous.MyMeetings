CREATE VIEW [Meetings].[v_MeetingGroupMembers]
AS
SELECT
    [MeetingGroupMember].MeetingGroupId,
    [MeetingGroupMember].MemberId,
    [MeetingGroupMember].RoleCode
FROM [Meetings].MeetingGroupMembers AS [MeetingGroupMember]
GO