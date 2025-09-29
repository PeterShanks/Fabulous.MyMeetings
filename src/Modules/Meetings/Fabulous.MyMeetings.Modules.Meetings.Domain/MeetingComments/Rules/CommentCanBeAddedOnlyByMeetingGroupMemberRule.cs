using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCanBeAddedOnlyByMeetingGroupMemberRule(MemberId authorId, MeetingGroup meetingGroup)
    : IBusinessRule
{
    public bool IsBroken() => !meetingGroup.IsMemberOfGroup(authorId);

    public string Message => "Only meeting attendee can add comments";
}