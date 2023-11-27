using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration
{
    internal class UserRegistrationConfirmedHandler: INotificationHandler<UserRegistrationConfirmedDomainEvent>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUserRepository _userRepository;

        public UserRegistrationConfirmedHandler(IUserRegistrationRepository userRegistrationRepository, IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            var userRegistration =
                await _userRegistrationRepository.GetByIdAsync(notification.UserRegistrationId);

            var user = userRegistration!.CreateUser();

            _userRepository.Add(user);
        }
    }
}
