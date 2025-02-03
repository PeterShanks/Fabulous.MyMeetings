using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Domain.Tokens;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

internal class
    SendUserRegistrationConfirmationEmailCommandHandler(
        IEmailService emailService,
        ITokenService tokenService,
        SiteSettings siteSettings) : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
{
    public async Task Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await tokenService.CreateAsync(request.UserRegistrationId, TokenTypeId.ConfirmEmail);

        var url = $"{siteSettings.SiteUrl}/api/account/{request.UserRegistrationId.Value}/confirm?token={token}";

        var content = $$"""
                      <!DOCTYPE html>
                      <html>
                      <head>
                          <meta charset="UTF-8">
                          <meta name="viewport" content="width=device-width, initial-scale=1.0">
                          <title>Confirm Your Account - MyMeetings</title>
                          <style>
                              body {
                                  font-family: Arial, sans-serif;
                                  background-color: #f4f4f4;
                                  margin: 0;
                                  padding: 0;
                              }
                              .container {
                                  max-width: 600px;
                                  margin: 20px auto;
                                  background: #ffffff;
                                  padding: 20px;
                                  border-radius: 10px;
                                  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
                                  text-align: center;
                              }
                              .header {
                                  background: #007BFF;
                                  padding: 15px;
                                  color: white;
                                  font-size: 24px;
                                  font-weight: bold;
                                  border-radius: 10px 10px 0 0;
                              }
                              .content {
                                  padding: 20px;
                                  font-size: 16px;
                                  color: #333;
                              }
                              .button {
                                  display: inline-block;
                                  padding: 15px 25px;
                                  margin-top: 20px;
                                  font-size: 18px;
                                  color: white;
                                  background: #28a745;
                                  text-decoration: none;
                                  border-radius: 5px;
                                  font-weight: bold;
                              }
                              .footer {
                                  margin-top: 20px;
                                  font-size: 12px;
                                  color: #777;
                              }
                          </style>
                      </head>
                      <body>
                          <div class="container">
                              <div class="header">MyMeetings</div>
                              <div class="content">
                                  <h2>Welcome to MyMeetings!</h2>
                                  <p>Hi {{request.FirstName}}</p>
                                  <p>Thank you for signing up. Please confirm your email address to start using your account.</p>
                                  <a href="{{url}}" class="button">Confirm Your Email</a>
                              </div>
                              <div class="footer">
                                  <p>If you did not create this account, please ignore this email.</p>
                                  <p>&copy; 2025 MyMeetings. All rights reserved.</p>
                              </div>
                          </div>
                      </body>
                      </html>
                      """;

        var emailMessage = new EmailMessage(
            request.Email,
            request.FirstName,
            "MyMeetings - Please confirm your registration",
            content);

        await emailService.SendEmail(emailMessage);
    }
}