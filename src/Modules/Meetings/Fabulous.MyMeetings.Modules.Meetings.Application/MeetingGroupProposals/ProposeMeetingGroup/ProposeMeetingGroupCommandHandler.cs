using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;

internal class ProposeMeetingGroupCommandHandler(
    IMeetingGroupProposalRepository meetingGroupProposalRepository,
    IMemberContext memberContext) : ICommandHandler<ProposeMeetingGroupCommand, Guid>
{
    public async Task<Guid> Handle(ProposeMeetingGroupCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = MeetingGroupProposal.ProposeNew(
            request.Name,
            request.Description,
            MeetingGroupLocation.CreateNew(request.LocationCity, request.LocationCountryCode),
            memberContext.MemberId);

        await meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

        return meetingGroupProposal.Id;
    }
}