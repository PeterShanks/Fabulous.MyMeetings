namespace Fabulous.MyMeetings.Identity.UserManagement
{
    public class UserManagementServiceSettings
    {
        public required string BaseUrl { get; set; }
        public required string AuthenticateUrl { get; set; }
        public required string PermissionsUrl { get; set; }
        public required string UserUrl { get; set; }

        public string GetPermissionsUrl(Guid userId)
        {
            return PermissionsUrl.Replace("{userId}", userId.ToString());
        }

        public string GetUserUrl(Guid userId)
        {
            return UserUrl.Replace("{userId}", userId.ToString());
        }
    }
}
