using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupMember: Entity
{
    public MemberId MemberId { get; }
    public MeetingGroupId MeetingGroupId { get; }
    public MeetingGroupMemberRole Role { get; }
    public bool IsActive { get; private set; }
    public DateTime JoinedDate { get; }
    public DateTime? LeaveDate { get; private set; }

    private MeetingGroupMember()
    {
        // FOR EF
    }

    private MeetingGroupMember(
        MeetingGroupId meetingGroupId,
        MemberId memberId,
        MeetingGroupMemberRole role)
    {
        MeetingGroupId = meetingGroupId;
        MemberId = memberId;
        Role = role;
        IsActive = true;
        JoinedDate = DateTime.UtcNow;

        AddDomainEvent(new NewMeetingGroupMemberJoinedDomainEvent(MeetingGroupId, MemberId, Role));
    }

    internal static MeetingGroupMember CreateMember(
        MeetingGroupId meetingGroupId,
        MemberId memberId,
        MeetingGroupMemberRole role)
    {
        return new MeetingGroupMember(meetingGroupId, memberId, role);
    }

    internal void Leave()
    {
        LeaveDate = DateTime.UtcNow;
        IsActive = false;
        AddDomainEvent(new MeetingGroupMemberLeftGroupDomainEvent(MeetingGroupId, MemberId));
    }

    internal bool IsMember(MemberId memberId) => MemberId == memberId;

    internal bool IsOrganizer(MemberId memberId) => IsMember(memberId) && Role == MeetingGroupMemberRole.Organizer;
}