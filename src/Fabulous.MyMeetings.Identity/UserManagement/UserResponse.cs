namespace Fabulous.MyMeetings.Identity.UserManagement;

public class UserResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}