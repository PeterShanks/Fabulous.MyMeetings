namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

public class UserDto
{
    public required Guid Id { get; set; }
    public required bool IsActive { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}