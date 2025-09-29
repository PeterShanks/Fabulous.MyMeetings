using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(int memberCommentLikesCount) : IBusinessRule
{
    public bool IsBroken() => memberCommentLikesCount > 0;

    public string Message => "Member cannot like one comment more than once.";
}