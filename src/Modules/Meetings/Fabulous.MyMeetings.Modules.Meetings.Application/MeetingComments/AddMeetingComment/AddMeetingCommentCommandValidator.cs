using FluentValidation;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;

public class AddMeetingCommentCommandValidator: AbstractValidator<AddMeetingCommentCommand>
{
    public AddMeetingCommentCommandValidator()
    {
        RuleFor(c => c.MeetingId).NotEmpty()
            .WithMessage("Id of meeting member cannot be empty.");

        RuleFor(c => c.Comment).NotNull().NotEmpty()
            .WithMessage("Comment cannot be null or empty.");
    }
}