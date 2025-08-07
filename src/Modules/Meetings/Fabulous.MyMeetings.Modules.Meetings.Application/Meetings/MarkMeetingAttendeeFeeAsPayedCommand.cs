
namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings;

public class MarkMeetingAttendeeFeeAsPayedCommand(Guid id, Guid memberId, Guid meetingId) : InternalCommand(id)
{
    public Guid MemberId { get; } = memberId;

    public Guid MeetingId { get; } = meetingId;
}