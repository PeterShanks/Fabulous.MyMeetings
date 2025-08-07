using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules;

internal class TokenCannotBeInvalidatedMoreThanOnceRule(bool isInvalidated): IBusinessRule
{
    public string Message => "Token cannot be invalidated more than once.";
    public bool IsBroken() => isInvalidated;
}