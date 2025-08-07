using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentTextMustBeProvidedRule(string comment) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrEmpty(comment);

    public string Message => "Comment text must be provided.";
}