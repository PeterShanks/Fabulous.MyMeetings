using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

public class MeetingCommentLikedDomainEvent(MeetingCommentId meetingCommentId, MemberId likerId) : DomainEvent
{
    public MeetingCommentId MeetingCommentId { get; } = meetingCommentId;

    public MemberId LikerId { get; } = likerId;
}