namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;

public class GetMeetingGroupProposalQuery(Guid meetingGroupProposalId): Query<MeetingGroupProposalDto>
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
}