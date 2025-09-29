using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule(MemberId attendeeId, List<MeetingAttendee> attendees)
    : IBusinessRule
{
    public bool IsBroken() => attendees.SingleOrDefault(x => x.IsActiveAttendee(attendeeId)) != null;

    public string Message => "Member is already an attendee of this meeting";
}