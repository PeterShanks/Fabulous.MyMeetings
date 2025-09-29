namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class AcceptMeetingGroupProposalCommand(Guid meetingGroupProposalId): Command
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
}