using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MemberCannotBeNotAttendeeTwiceRule(List<MeetingNotAttendee> notAttendees, MemberId memberId)
    : IBusinessRule
{
    public bool IsBroken() => notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(memberId)) != null;

    public string Message => "Member cannot be active not attendee twice";
}