using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class MeetingCanBeOrganizedOnlyByPayedGroupRule(DateTime? paymentDateTo) : IBusinessRule
{
    public string Message => "Meeting can be organized only by payed group";
    public bool IsBroken() => !paymentDateTo.HasValue || paymentDateTo.Value < DateTime.UtcNow;
}