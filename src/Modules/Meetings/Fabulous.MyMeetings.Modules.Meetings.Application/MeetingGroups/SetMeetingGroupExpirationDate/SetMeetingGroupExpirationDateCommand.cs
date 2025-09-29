namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;

public class SetMeetingGroupExpirationDateCommand(Guid id, Guid meetingGroupId, DateTime dateTo) : InternalCommand(id)
{
    public Guid MeetingGroupId { get; } = meetingGroupId;
    public DateTime DateTo { get; } = dateTo;
}