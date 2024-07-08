namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

public class UserRegistrationId : TypedId
{
    public UserRegistrationId(Guid value) : base(value)
    {
    }

    public static UserRegistrationId New => new(Guid.NewGuid());
}