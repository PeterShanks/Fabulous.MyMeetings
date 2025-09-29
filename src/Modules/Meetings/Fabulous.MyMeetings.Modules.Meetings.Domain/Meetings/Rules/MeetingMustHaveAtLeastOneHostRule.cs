using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingMustHaveAtLeastOneHostRule(int meetingHostNumber) : IBusinessRule
{
    public bool IsBroken() => meetingHostNumber == 0;

    public string Message => "Meeting must have at least one host";
}