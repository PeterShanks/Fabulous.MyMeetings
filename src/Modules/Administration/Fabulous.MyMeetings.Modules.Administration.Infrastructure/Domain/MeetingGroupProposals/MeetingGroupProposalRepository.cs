using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals;

public class MeetingGroupProposalRepository(AdministrationContext context): IMeetingGroupProposalRepository
{
    public Task AddAsync(MeetingGroupProposal meetingGroupProposal)
    {
        return context.MeetingGroupProposals.AddAsync(meetingGroupProposal).AsTask();
    }

    public Task<MeetingGroupProposal?> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId)
    {
        return context.MeetingGroupProposals.FindAsync(meetingGroupProposalId).AsTask();
    }
}