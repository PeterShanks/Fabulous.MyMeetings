using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingWaitlistMember: Entity
{
    public MemberId MemberId { get; }
    public MeetingId MeetingId { get; }
    public DateTime SignUpDate { get; }
    public bool IsSignedOff { get; private set; }
    public DateTime? SignOffDate { get; private set; }
    public bool IsMovedToAttendees { get; private set; }
    public DateTime? MovedToAttendeesDate { get; private set; }

    private MeetingWaitlistMember()
    {
        // Only for EF.
    }

    private MeetingWaitlistMember(MemberId memberId, MeetingId meetingId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
        SignUpDate = DateTime.UtcNow;
        IsMovedToAttendees = false;

        AddDomainEvent(new MeetingWaitlistMemberAddedDomainEvent(MeetingId, memberId));
    }

    internal static MeetingWaitlistMember CreateNew(MeetingId meetingId, MemberId memberId)
    {
        return new MeetingWaitlistMember(memberId, meetingId);
    }

    internal void MarkIsMovedToAttendees()
    {
        IsMovedToAttendees = true;
        MovedToAttendeesDate = DateTime.UtcNow;
    }

    internal bool IsActiveOnWaitlist(MemberId memberId)
    {
        return MeetingId == memberId && IsActive();
    }

    internal bool IsActive()
    {
        return !IsSignedOff && !IsMovedToAttendees;
    }

    internal void SignOff()
    {
        IsSignedOff = true;
        SignOffDate = DateTime.UtcNow;
        AddDomainEvent(new MemberSignedOffFromMeetingWaitlistDomainEvent(MeetingId, MemberId));
    }
}