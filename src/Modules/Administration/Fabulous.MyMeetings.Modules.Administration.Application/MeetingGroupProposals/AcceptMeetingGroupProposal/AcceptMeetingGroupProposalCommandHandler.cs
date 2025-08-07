using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Administration.Domain.Users;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class AcceptMeetingGroupProposalCommandHandler(
    IMeetingGroupProposalRepository meetingGroupProposalRepository,
    IUserContext userContext): ICommandHandler<AcceptMeetingGroupProposalCommand>
{
    public async Task Handle(AcceptMeetingGroupProposalCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = await meetingGroupProposalRepository.GetByIdAsync(new MeetingGroupProposalId(request.MeetingGroupProposalId));

        if (meetingGroupProposal is null)
            throw new InvalidCommandException(["No meeting group proposal matches this id"]);

        meetingGroupProposal.Accept(userContext.UserId);
    }
}