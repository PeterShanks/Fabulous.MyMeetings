
namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;

public class MeetingGroupProposalRejectionMustHaveAReasonRule(string reason) : IBusinessRule
{
    public string Message => "Meeting group proposal rejection must have a reason";

    public bool IsBroken() => string.IsNullOrEmpty(reason);
}