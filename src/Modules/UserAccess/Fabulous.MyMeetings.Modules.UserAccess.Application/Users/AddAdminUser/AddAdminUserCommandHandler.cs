using Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser;

internal class AddAdminUserCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager) : ICommandHandler<AddAdminUserCommand>
{
    public Task Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = passwordManager.HashPassword(request.Password);

        var user = User.CreateAdmin(
            hashedPassword,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Name
        );

        userRepository.Add(user);

        return Task.CompletedTask;
    }
}