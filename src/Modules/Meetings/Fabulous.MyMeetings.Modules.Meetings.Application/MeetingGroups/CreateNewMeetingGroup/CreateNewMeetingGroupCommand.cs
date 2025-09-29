using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;

public class CreateNewMeetingGroupCommand(Guid id, MeetingGroupProposalId meetingGroupProposalId): InternalCommand(id)
{
    public MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;
}