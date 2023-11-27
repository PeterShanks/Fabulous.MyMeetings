namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationId: TypedId
    {
        public static UserRegistrationId New => new UserRegistrationId(Guid.NewGuid());

        public UserRegistrationId(Guid value) : base(value)
        {
        }
    }
}
