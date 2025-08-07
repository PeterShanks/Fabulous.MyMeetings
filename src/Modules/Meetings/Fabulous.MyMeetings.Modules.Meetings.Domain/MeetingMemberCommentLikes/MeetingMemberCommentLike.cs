using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;

public class MeetingMemberCommentLike: Entity, IAggregateRoot
{
    public MeetingMemberCommentLikeId Id { get; }
    public MeetingCommentId MeetingCommentId { get; }
    public MemberId MemberId { get; }
    public DateTime LikeDate { get; }

    private MeetingMemberCommentLike(MeetingCommentId meetingCommentId, MemberId memberId)
    {
        Id = new MeetingMemberCommentLikeId(Guid.CreateVersion7());
        MeetingCommentId = meetingCommentId;
        MemberId = memberId;
        LikeDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingCommentLikedDomainEvent(MeetingCommentId, MemberId));
    }

    public void Remove()
    {
        AddDomainEvent(new MeetingCommentUnlikedDomainEvent(MeetingCommentId, MemberId));
    }

    public static MeetingMemberCommentLike Create(MeetingCommentId meetingCommentId, MemberId memberId)
    {
        return new MeetingMemberCommentLike(meetingCommentId, memberId);
    }
}