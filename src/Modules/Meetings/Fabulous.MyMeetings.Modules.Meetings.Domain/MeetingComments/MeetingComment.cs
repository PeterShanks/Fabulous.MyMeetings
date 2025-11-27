using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;

public class MeetingComment: Entity, IAggregateRoot
{
    public MeetingCommentId Id { get; }
    public MeetingId MeetingId { get; }
    public MemberId AuthorId { get; }
    public MeetingCommentId? InReplyToCommentId { get; }
    public string Comment { get; private set; }
    public DateTime CreateDate { get; }
    public DateTime? EditDate { get; private set; }
    public bool IsRemoved { get; private set; }
    public string? RemovedByReason { get; private set; }

    private MeetingComment(
        MeetingId meetingId,
        MemberId authorId,
        string comment,
        MeetingCommentId? inReplyToCommentId,
        MeetingCommentingConfiguration meetingCommentingConfiguration,
        MeetingGroup meetingGroup)
    {
        CheckRule(new CommentTextMustBeProvidedRule(comment));
        CheckRule(new CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));
        CheckRule(new CommentCanBeAddedOnlyByMeetingGroupMemberRule(authorId, meetingGroup));

        Id = new MeetingCommentId(Guid.CreateVersion7());
        MeetingId = meetingId;
        AuthorId = authorId;
        Comment = comment;

        InReplyToCommentId = inReplyToCommentId;

        CreateDate = DateTime.UtcNow;
        EditDate = null;
        IsRemoved = false;
        RemovedByReason = null;

        if (InReplyToCommentId is null)
            AddDomainEvent(new MeetingCommentAddedDomainEvent(Id, meetingId, comment));
        else
            AddDomainEvent(new ReplyToMeetingCommentAddedDomainEvent(Id, inReplyToCommentId!, comment));
    }

    private MeetingComment()
    {
        // Only for EF.
    }

    public void Edit(MemberId editorId, string editedComment, MeetingCommentingConfiguration meetingCommentingConfiguration)
    {
        CheckRule(new CommentTextMustBeProvidedRule(editedComment));
        CheckRule(new MeetingCommentCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
        CheckRule(new CommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));

        Comment = editedComment;
        EditDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingCommentEditedDomainEvent(Id, Comment));
    }

    public void Remove(MemberId removingMemberId, MeetingGroup meetingGroup, string? reason = null)
    {
        CheckRule(new MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(meetingGroup, AuthorId, removingMemberId));
        CheckRule(new RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(meetingGroup, removingMemberId, reason));

        IsRemoved = true;
        RemovedByReason = reason;

        AddDomainEvent(new MeetingCommentRemovedDomainEvent(Id));
    }

    public MeetingComment Reply(MemberId replierId, string reply, MeetingGroup meetingGroup,
        MeetingCommentingConfiguration meetingCommentingConfiguration) =>
            new(MeetingId, replierId, reply, Id, meetingCommentingConfiguration, meetingGroup);

    public MeetingMemberCommentLike Like(
        MemberId likerId,
        MeetingGroupMemberData likerMeetingGroupMember,
        int meetingMemberCommentLikesCount)
    {
        CheckRule(new CommentCanBeLikedOnlyByMeetingGroupMemberRule(likerMeetingGroupMember));
        CheckRule(new CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(meetingMemberCommentLikesCount));

        return MeetingMemberCommentLike.Create(Id, likerId);
    }

    internal static MeetingComment Create(
        MeetingId meetingId,
        MemberId authorId,
        string comment,
        MeetingGroup meetingGroup,
        MeetingCommentingConfiguration meetingCommentingConfiguration)
            => new(meetingId, authorId, comment, null, meetingCommentingConfiguration, meetingGroup);
}