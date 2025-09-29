using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings;

public class MeetingRepository(MeetingsContext context): IMeetingRepository
{
    public Task AddAsync(Meeting meeting)
        => context.Meetings.AddAsync(meeting).AsTask();

    public Task<Meeting?> GetByIdAsync(MeetingId id)
        => context.Meetings
            .FindAsync(id)
            .AsTask();
}