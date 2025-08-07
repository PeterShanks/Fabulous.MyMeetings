using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class AcceptMeetingGroupProposalCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository): ICommandHandler<AcceptMeetingGroupProposalCommand>
{
    public async Task Handle(AcceptMeetingGroupProposalCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = await meetingGroupProposalRepository.GetByIdAsync(new MeetingGroupProposalId(request.MeetingGroupProposalId));

        meetingGroupProposal.Accept();
    }
}