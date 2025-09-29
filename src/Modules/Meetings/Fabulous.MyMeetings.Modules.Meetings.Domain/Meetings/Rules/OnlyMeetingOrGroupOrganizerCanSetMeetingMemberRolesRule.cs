using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(
    MemberId settingMemberId,
    MeetingGroup meetingGroup,
    List<MeetingAttendee> attendees)
    : IBusinessRule
{
    public bool IsBroken()
    {
        var settingMember = attendees.SingleOrDefault(x => x.IsActiveAttendee(settingMemberId));

        var isHost = settingMember != null && settingMember.IsActiveHost();
        var isOrganizer = meetingGroup.IsOrganizer(settingMemberId);

        return !isHost && !isOrganizer;
    }

    public string Message => "Only meeting host or group organizer can set meeting member roles";
}