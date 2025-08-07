using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations;

public class MeetingCommentingConfigurationRepository(MeetingsContext context)
    : IMeetingCommentingConfigurationRepository
{
    public Task AddAsync(MeetingCommentingConfiguration meetingCommentingConfiguration)
        => context.MeetingCommentingConfigurations.AddAsync(meetingCommentingConfiguration)
            .AsTask();

    public Task<MeetingCommentingConfiguration?> GetByMeetingIdAsync(MeetingId meetingId)
        => context
            .MeetingCommentingConfigurations
            .FirstOrDefaultAsync(x => x.MeetingId == meetingId);
}