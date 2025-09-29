using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

public interface IMeetingCommentingConfigurationRepository
{
    Task AddAsync(MeetingCommentingConfiguration meetingCommentingConfiguration);

    Task<MeetingCommentingConfiguration?> GetByMeetingIdAsync(MeetingId meetingId);
}