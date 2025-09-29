using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;

public class MeetingCommentingDisabledDomainEvent(MeetingId meetingId) : DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
}