using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

internal class
    SendUserRegistrationConfirmationEmailCommandHandler(IEmailSender emailSender) : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
{
    public Task Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var link = $"<a href=\"{request.UserRegistrationId.Value}\">link</a>";

        var content = $"Welcome to MyMeetings application! Please confirm your registration using this {link}.";

        var emailMessage = new EmailMessage(
            request.Email,
            "MyMeetings - Please confirm your registration",
            content);

        return emailSender.SendEmail(emailMessage);
    }
}