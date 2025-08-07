using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule(
    MeetingCommentingConfiguration meetingCommentingConfiguration)
    : IBusinessRule
{
    public bool IsBroken() => !meetingCommentingConfiguration.IsCommentingEnabled;

    public string Message => "Commenting for meeting is disabled.";
}