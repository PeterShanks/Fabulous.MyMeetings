using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;

public class CreateNewMeetingGroupCommandHandler(
    IMeetingGroupRepository meetingGroupRepository,
    IMeetingGroupProposalRepository meetingGroupProposalRepository) : ICommandHandler<CreateNewMeetingGroupCommand>
{
    public async Task Handle(CreateNewMeetingGroupCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = await meetingGroupProposalRepository.GetByIdAsync(request.MeetingGroupProposalId);

        var meetingGroup = meetingGroupProposal.CreateMeetingGroup();

        await meetingGroupRepository.AddAsync(meetingGroup);
    }
}