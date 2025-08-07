namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;

public class DisableMeetingCommentingConfigurationCommand(Guid meetingId): Command
{
    public Guid MeetingId { get; } = meetingId;
}