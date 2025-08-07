using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;

public class SendMeetingAttendeeAddedEmailCommand(Guid id, MemberId attendeeId, MeetingId meetingId) : InternalCommand(id)
{
    public MemberId AttendeeId { get; } = attendeeId;
    public MeetingId MeetingId { get; } = meetingId;
}