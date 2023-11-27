using Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser
{
    internal class AddAdminUserCommandHandler: ICommandHandler<AddAdminUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;

        public AddAdminUserCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
        }

        public Task Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordManager.HashPassword(request.Password);

            var user = User.CreateAdmin(
                request.Login,
                hashedPassword,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Name
            );

            _userRepository.Add(user);

            return Task.CompletedTask;
        }
    }
}
