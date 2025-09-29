using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups;

public class MeetingGroupRepository(MeetingsContext context): IMeetingGroupRepository
{
    public Task AddAsync(MeetingGroup meetingGroup)
        => context.MeetingGroups.AddAsync(meetingGroup).AsTask();

    public Task<MeetingGroup?> GetByIdAsync(MeetingGroupId meetingGroupId)
        => context.MeetingGroups
            .FindAsync(meetingGroupId)
            .AsTask();
}