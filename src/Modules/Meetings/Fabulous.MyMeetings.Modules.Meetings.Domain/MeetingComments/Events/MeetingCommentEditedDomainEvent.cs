using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;

public class MeetingCommentEditedDomainEvent(MeetingCommentId meetingCommentId, string editedComment) : DomainEvent
{
    public MeetingCommentId MeetingCommentId { get; } = meetingCommentId;

    public string EditedComment { get; } = editedComment;
}