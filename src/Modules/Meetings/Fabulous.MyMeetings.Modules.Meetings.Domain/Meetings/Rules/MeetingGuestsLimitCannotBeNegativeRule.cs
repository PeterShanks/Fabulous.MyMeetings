using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingGuestsLimitCannotBeNegativeRule(int guestsLimit) : IBusinessRule
{
    public bool IsBroken() => guestsLimit < 0;

    public string Message => "Guests limit cannot be negative";
}