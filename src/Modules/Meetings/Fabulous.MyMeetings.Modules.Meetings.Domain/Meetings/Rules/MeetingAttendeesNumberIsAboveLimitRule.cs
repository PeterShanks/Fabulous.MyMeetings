using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingAttendeesNumberIsAboveLimitRule(
    int? attendeesLimit,
    int allActiveAttendeesWithGuestsNumber,
    int guestsNumber)
    : IBusinessRule
{
    public bool IsBroken() => attendeesLimit.HasValue &&
                              attendeesLimit.Value < allActiveAttendeesWithGuestsNumber + 1 + guestsNumber;

    public string Message => "Meeting attendees number is above limit";
}