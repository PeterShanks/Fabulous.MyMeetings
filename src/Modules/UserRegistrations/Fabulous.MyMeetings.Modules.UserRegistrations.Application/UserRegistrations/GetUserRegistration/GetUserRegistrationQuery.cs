using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.GetUserRegistration;

public class GetUserRegistrationQuery(Guid userRegistrationId) : Query<UserRegistrationDto>
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
}