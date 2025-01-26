namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

public class UserDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}