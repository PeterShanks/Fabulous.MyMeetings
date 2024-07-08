using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

internal class
    SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
{
    private readonly IEmailSender _emailSender;

    public SendUserRegistrationConfirmationEmailCommandHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var link = $"<a href=\"{request.ConfirmLink}{request.UserRegistrationId.Value}\">link</a>";

        var content = $"Welcome to MyMeetings application! Please confirm your registration using this {link}.";

        var emailMessage = new EmailMessage(
            request.Email,
            "MyMeetings - Please confirm your registration",
            content);

        return _emailSender.SendEmail(emailMessage);
    }
}