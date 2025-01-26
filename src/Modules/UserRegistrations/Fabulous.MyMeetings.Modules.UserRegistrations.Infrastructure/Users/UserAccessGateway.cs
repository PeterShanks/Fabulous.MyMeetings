using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Users.CreateUser;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Users
{
    public class UserAccessGateway(IUserAccessModule userAccessModule) : IUserCreator
    {
        public async Task Create(
            Guid userRegistrationId,
            string password,
            string email,
            string firstName,
            string lastName)
        {
            await userAccessModule.ExecuteCommandAsync(new CreateUserCommand(
                userRegistrationId,
                email,
                firstName,
                lastName,
                password));
        }
    }
}
