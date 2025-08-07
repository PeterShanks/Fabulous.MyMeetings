using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules;

public class TokenCannotBeInvalidatedWhenItIsUsedRule(bool isUsed): IBusinessRule
{
    public string Message => "Token cannot be invalidated when it is used.";
    public bool IsBroken() => isUsed;
}