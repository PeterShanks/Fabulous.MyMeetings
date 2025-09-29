using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals;

public class MeetingGroupProposalRepository(MeetingsContext context): IMeetingGroupProposalRepository
{
    public Task AddAsync(MeetingGroupProposal meetingGroupProposal)
        => context.MeetingGroupProposals
            .AddAsync(meetingGroupProposal)
            .AsTask();

    public Task<MeetingGroupProposal?> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId)
        => context.MeetingGroupProposals
            .FindAsync(meetingGroupProposalId)
            .AsTask();
}