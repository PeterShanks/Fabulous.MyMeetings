using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingGuestsNumberIsAboveLimitRule(int guestsLimit, int guestsNumber) : IBusinessRule
{
    public bool IsBroken() => guestsLimit > 0 && guestsLimit < guestsNumber;

    public string Message => "Meeting guests number is above limit";
}