using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupMemberRole : ValueObject
{
    public static MeetingGroupMemberRole Organizer => new MeetingGroupMemberRole("Organizer");
    public static MeetingGroupMemberRole Member => new MeetingGroupMemberRole("Member");
    public static MeetingGroupMemberRole Of(string roleCode) => new MeetingGroupMemberRole(roleCode);
    public string Value { get; }

    private MeetingGroupMemberRole(string value)
    {
        Value = value;
    }
}