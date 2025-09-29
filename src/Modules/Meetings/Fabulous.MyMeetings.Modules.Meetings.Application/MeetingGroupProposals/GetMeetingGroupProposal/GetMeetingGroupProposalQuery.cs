namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

public class GetMeetingGroupProposalQuery(Guid meetingGroupProposalId):Query<MeetingGroupProposalDto>
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
}