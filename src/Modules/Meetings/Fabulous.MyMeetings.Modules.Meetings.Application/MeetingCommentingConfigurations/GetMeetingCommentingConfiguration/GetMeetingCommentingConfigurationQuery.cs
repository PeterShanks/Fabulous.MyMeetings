namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;

public class GetMeetingCommentingConfigurationQuery(Guid meetingId): Query<MeetingCommentingConfigurationDto?>
{
    public Guid MeetingId { get; } = meetingId;
}