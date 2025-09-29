using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(int? attendeesLimit, int guestsLimit)
    : IBusinessRule
{
    public bool IsBroken() => attendeesLimit <= guestsLimit;

    public string Message => "Attendees limit must be greater than guests limit";
}