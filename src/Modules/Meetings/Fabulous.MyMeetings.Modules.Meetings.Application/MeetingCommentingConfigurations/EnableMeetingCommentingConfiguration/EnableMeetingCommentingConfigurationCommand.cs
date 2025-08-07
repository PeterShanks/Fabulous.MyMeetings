namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration;

public class EnableMeetingCommentingConfigurationCommand(Guid meetingId): Command
{
    public Guid MeetingId { get; } = meetingId;
}