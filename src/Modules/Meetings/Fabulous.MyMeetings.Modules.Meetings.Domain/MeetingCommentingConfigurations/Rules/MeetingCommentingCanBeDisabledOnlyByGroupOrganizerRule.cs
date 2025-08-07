using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;

public class MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(
    MemberId disablingMemberId,
    MeetingGroup meetingGroup) : IBusinessRule
{
    public bool IsBroken() => !meetingGroup.IsOrganizer(disablingMemberId);

    public string Message => "Commenting for meeting can be disabled only by group organizer";
}