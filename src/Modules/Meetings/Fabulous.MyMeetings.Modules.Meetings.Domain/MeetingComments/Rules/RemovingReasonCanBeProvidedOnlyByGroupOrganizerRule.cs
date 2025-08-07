using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(
    MeetingGroup meetingGroup,
    MemberId removingMemberId,
    string? removingReason)
    : IBusinessRule
{
    public bool IsBroken() =>
        !string.IsNullOrEmpty(removingReason) && !meetingGroup.IsOrganizer(removingMemberId);

    public string Message => "Only group organizer can provide comment's removing reason.";
}