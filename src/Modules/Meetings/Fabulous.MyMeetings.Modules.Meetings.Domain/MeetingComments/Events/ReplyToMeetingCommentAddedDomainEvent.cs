using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;

public class ReplyToMeetingCommentAddedDomainEvent(
    MeetingCommentId meetingCommentId,
    MeetingCommentId inReplyToCommentId,
    string reply)
    : DomainEvent
{
    public MeetingCommentId MeetingCommentId { get; } = meetingCommentId;

    public MeetingCommentId InReplyToCommentId { get; } = inReplyToCommentId;

    public string Reply { get; } = reply;
}