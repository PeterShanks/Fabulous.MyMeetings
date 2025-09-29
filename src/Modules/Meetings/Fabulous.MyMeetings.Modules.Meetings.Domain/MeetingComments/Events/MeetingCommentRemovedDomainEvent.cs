using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;

public class MeetingCommentRemovedDomainEvent(MeetingCommentId meetingCommentId) : DomainEvent
{
    public MeetingCommentId MeetingCommentId { get; } = meetingCommentId;
}