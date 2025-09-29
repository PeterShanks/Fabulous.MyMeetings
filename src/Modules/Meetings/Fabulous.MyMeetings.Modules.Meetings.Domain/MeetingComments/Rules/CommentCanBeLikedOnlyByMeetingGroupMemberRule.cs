using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCanBeLikedOnlyByMeetingGroupMemberRule(MeetingGroupMemberData? likerMeetingGroupMember)
    : IBusinessRule
{
    public bool IsBroken() => likerMeetingGroupMember == null;

    public string Message => "Comment can be liked only by meeting group member.";
}