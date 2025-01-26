namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.GetUserRegistration;

public class UserRegistrationDto
{
    public Guid Id { get; init; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Name { get; set; }

    public required string StatusCode { get; set; }
    public required string Password { get; set; }
}