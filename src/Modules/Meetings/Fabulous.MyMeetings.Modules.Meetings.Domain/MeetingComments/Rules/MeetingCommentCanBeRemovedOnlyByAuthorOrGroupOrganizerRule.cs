using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(
    MeetingGroup meetingGroup,
    MemberId authorId,
    MemberId removingMemberId)
    : IBusinessRule
{
    public bool IsBroken() => removingMemberId != authorId && !meetingGroup.IsOrganizer(removingMemberId);

    public string Message => "Only author of comment or group organizer can remove meeting comment.";
}