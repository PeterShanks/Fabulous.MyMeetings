using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingAttendeeChangedDecisionDomainEvent(MemberId memberId, MeetingId meetingId) : DomainEvent
{
    public MemberId MemberId { get; } = memberId;
    public MeetingId MeetingId { get; } = meetingId;
}