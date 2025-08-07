using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;

public class MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(MeetingGroupProposalStatus actualStatus): IBusinessRule
{
    public bool IsBroken() => actualStatus.IsAccepted;
    public string Message => "Meeting group proposal cannot be accepted more than once";
}