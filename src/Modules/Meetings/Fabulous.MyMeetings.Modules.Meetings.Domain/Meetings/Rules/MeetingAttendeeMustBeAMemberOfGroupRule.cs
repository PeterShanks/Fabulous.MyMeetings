using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingAttendeeMustBeAMemberOfGroupRule(MemberId attendeeId, MeetingGroup meetingGroup) : IBusinessRule
{
    public bool IsBroken()
    {
        return !meetingGroup.IsMemberOfGroup(attendeeId);
    }

    public string Message => "Meeting attendee must be a member of group";
}