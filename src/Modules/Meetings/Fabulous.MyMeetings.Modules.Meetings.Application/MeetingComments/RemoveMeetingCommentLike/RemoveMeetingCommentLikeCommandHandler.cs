using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;

public class RemoveMeetingCommentLikeCommandHandler(
    IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
    IMemberContext memberContext): ICommandHandler<RemoveMeetingCommentLikeCommand>
{
    public async Task Handle(RemoveMeetingCommentLikeCommand request, CancellationToken cancellationToken)
    {
        var commentLike = await meetingMemberCommentLikesRepository.GetAsync(memberContext.MemberId,
            new MeetingCommentId(request.MeetingCommentId));

        if (commentLike is null)
            throw new InvalidCommandException(["Meeting comment like for removing must exist."]);

        commentLike.Remove();

        meetingMemberCommentLikesRepository.Remove(commentLike);
    }
}