using FluentValidation;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;

public class EditMeetingCommentCommandValidator: AbstractValidator<EditMeetingCommentCommand>
{
    public EditMeetingCommentCommandValidator()
    {
        RuleFor(c => c.EditedComment)
            .NotEmpty()
            .WithMessage("Comment cannot be null or empty.");
    }
}