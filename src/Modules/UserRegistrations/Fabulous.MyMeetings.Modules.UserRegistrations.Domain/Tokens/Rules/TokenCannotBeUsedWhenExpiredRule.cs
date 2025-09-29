using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules;

public class TokenCannotBeUsedWhenExpiredRule(DateTime expiresAt): IBusinessRule
{
    public string Message => "Token cannot be used when expired";
    public bool IsBroken() => DateTime.UtcNow >= expiresAt;
}