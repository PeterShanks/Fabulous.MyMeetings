namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class AcceptMeetingGroupProposalCommand(Guid id, Guid meetingGroupProposalId): InternalCommand(id)
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
}