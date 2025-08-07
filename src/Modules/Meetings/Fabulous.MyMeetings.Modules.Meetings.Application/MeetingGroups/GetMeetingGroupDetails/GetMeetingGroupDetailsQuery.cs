namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;

public class GetMeetingGroupDetailsQuery(Guid meetingGroupId) : Query<MeetingGroupDetailsDto>
{
    public Guid MeetingGroupId { get; } = meetingGroupId;
}