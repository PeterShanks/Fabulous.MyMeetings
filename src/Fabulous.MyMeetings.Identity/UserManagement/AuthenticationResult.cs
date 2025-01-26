namespace Fabulous.MyMeetings.Identity.UserManagement
{
    public class AuthenticationResult
    {
        public required bool IsAuthenticated { get; set; }

        public string? AuthenticationError { get; set; }
        public Guid? UserId { get; set; }
    }
}
