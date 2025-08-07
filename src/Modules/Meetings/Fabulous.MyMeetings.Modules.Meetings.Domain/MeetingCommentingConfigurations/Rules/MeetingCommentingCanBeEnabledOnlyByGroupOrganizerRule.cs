using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;

public class MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(
    MemberId enablingMemberId,
    MeetingGroup meetingGroup) : IBusinessRule
{
    public bool IsBroken() => !meetingGroup.IsOrganizer(enablingMemberId);

    public string Message => "Commenting for meeting can be enabled only by group organizer";
}