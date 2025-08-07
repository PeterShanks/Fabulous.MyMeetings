using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;

public class MeetingCommentAddedDomainEvent(MeetingCommentId meetingCommentId, MeetingId meetingId, string comment)
    : DomainEvent
{
    public MeetingCommentId MeetingCommentId { get; } = meetingCommentId;

    public MeetingId MeetingId { get; } = meetingId;

    public string Comment { get; } = comment;
}