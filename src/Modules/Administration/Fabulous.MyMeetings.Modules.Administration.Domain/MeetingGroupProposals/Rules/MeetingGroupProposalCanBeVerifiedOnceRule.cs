namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;

public class MeetingGroupProposalCanBeVerifiedOnceRule(MeetingGroupProposalDecision actualDecision) : IBusinessRule
{
    public string Message => "Meeting group proposal can be verified only once";

    public bool IsBroken() => actualDecision != MeetingGroupProposalDecision.NoDecision;
}