using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

public class UserRegistrationId(Guid value) : TypedId(value)
{
    public static UserRegistrationId New => new(Guid.CreateVersion7());
}