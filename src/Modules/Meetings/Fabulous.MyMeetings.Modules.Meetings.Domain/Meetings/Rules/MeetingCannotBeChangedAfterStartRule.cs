using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingCannotBeChangedAfterStartRule(MeetingTerm meetingTerm) : IBusinessRule
{
    public bool IsBroken() => meetingTerm.IsAfterStart();

    public string Message => "Meeting cannot be changed after start";
}