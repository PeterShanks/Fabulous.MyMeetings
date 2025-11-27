using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupMemberRole : ValueObject
{
    public static MeetingGroupMemberRole Organizer => new("Organizer");
    public static MeetingGroupMemberRole Member => new("Member");
    public static MeetingGroupMemberRole Of(string roleCode) => new(roleCode);
    public string Value { get; }

    private MeetingGroupMemberRole(string value)
    {
        Value = value;
    }
}