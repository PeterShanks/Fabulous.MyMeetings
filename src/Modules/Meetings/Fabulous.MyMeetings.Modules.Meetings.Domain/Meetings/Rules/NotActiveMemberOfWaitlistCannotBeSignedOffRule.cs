using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class NotActiveMemberOfWaitlistCannotBeSignedOffRule(
    List<MeetingWaitlistMember> waitlistMembers,
    MemberId memberId)
    : IBusinessRule
{
    public bool IsBroken() => waitlistMembers.SingleOrDefault(x => x.IsActiveOnWaitlist(memberId)) == null;

    public string Message => "Not active member of waitlist cannot be signed off";
}