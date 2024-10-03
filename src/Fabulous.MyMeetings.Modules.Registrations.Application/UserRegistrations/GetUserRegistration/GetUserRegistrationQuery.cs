using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.GetUserRegistration;

public class GetUserRegistrationQuery(Guid userRegistrationId) : Query<UserRegistrationDto>
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
}