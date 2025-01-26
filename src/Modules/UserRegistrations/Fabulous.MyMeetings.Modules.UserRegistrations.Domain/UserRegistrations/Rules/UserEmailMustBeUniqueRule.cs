using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Rules;

public class UserEmailMustBeUniqueRule : IBusinessRule
{
    private readonly string _email;
    private readonly IUsersCounter _usersCounter;

    internal UserEmailMustBeUniqueRule(IUsersCounter usersCounter, string email)
    {
        _usersCounter = usersCounter;
        _email = email;
    }

    public bool IsBroken()
    {
        return _usersCounter.CountUsersWithEmail(_email) > 0;
    }

    public string Message => "User Login must be unique";
}