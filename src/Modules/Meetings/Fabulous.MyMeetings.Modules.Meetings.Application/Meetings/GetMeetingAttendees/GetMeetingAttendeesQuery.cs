namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees;

public class GetMeetingAttendeesQuery(Guid meetingId): Query<IEnumerable<MeetingAttendeeDto>>
{
    public Guid MeetingId { get; } = meetingId;
}