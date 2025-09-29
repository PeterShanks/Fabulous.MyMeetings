using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingAttendeesLimitCannotBeNegativeRule(int? attendeesLimit) : IBusinessRule
{
    public bool IsBroken() => attendeesLimit is < 0;

    public string Message => "Attendees limit cannot be negative";
}