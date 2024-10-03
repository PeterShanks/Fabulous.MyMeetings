using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : ICommandHandler<CreateUserCommand>
    {

        public Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = User.CreateUser(
                command.UserId,
                command.Login,
                command.Password,
                command.Email,
                command.FirstName,
                command.LastName);

            userRepository.Add(user);

            return Task.CompletedTask;
        }
    }
}
