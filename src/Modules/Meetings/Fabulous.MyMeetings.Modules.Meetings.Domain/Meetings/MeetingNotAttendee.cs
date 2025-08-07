using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingNotAttendee: Entity
{
    public MemberId MemberId { get; }
    public MeetingId MeetingId { get; }
    public DateTime DecisionDate { get; }
    public bool DecisionChanged { get; private set; }
    public DateTime? DecisionChangeDate { get; private set; }

    private MeetingNotAttendee()
    {
        // Only for EF.
    }

    internal static MeetingNotAttendee CreateNew(MeetingId meetingId, MemberId memberId)
    {
        return new MeetingNotAttendee(meetingId, memberId);
    }

    private MeetingNotAttendee(MeetingId meetingId, MemberId memberId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
        DecisionDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingNotAttendeeAddedDomainEvent(meetingId, memberId));
    }

    internal bool IsActiveNotAttendee(MemberId memberId)
        => !DecisionChanged && memberId == MemberId;

    internal void ChangeDecision()
    {
        DecisionChanged = true;
        DecisionChangeDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingNotAttendeeChangedDecisionDomainEvent(MemberId, MeetingId));
    }
}