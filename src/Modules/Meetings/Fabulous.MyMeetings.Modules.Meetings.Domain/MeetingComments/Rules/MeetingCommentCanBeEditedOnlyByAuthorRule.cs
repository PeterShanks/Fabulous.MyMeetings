using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class MeetingCommentCanBeEditedOnlyByAuthorRule(MemberId authorId, MemberId editorId) : IBusinessRule
{
    public bool IsBroken() => editorId != authorId;

    public string Message => "Only the author of a comment can edit it.";
}