using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Administration.Domain.Users;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;

public class RequestMeetingGroupProposalVerificationCommandHandler(
    IMeetingGroupProposalRepository meetingGroupProposalRepository): ICommandHandler<RequestMeetingGroupProposalVerificationCommand, Guid>
{
    public async Task<Guid> Handle(RequestMeetingGroupProposalVerificationCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
            request.MeetingGroupProposalId,
            request.Name,
            request.Description,
            MeetingGroupLocation.Create(request.LocationCity, request.LocationCountryCode),
            new UserId(request.ProposalUserId),
            request.ProposalDate);

        await meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

        return meetingGroupProposal.Id;
    }
}