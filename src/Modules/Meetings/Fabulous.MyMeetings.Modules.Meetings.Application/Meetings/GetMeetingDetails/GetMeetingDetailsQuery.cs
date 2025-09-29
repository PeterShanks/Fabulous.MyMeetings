
namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails;

public class GetMeetingDetailsQuery(Guid meetingId) : Query<MeetingDetailsDto>
{
    public Guid MeetingId { get; } = meetingId;
}