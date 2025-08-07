using Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.CreateUser;

public class CreateUserCommandHandler(IPasswordManager passwordManager, IUserRepository userRepository) : ICommandHandler<CreateUserCommand>
{

    public Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var hashedPassword = passwordManager.HashPassword(command.Password);

        var user = User.CreateUser(
            command.UserId,
            hashedPassword,
            command.Email,
            command.FirstName,
            command.LastName);

        userRepository.Add(user);

        return Task.CompletedTask;
    }
}