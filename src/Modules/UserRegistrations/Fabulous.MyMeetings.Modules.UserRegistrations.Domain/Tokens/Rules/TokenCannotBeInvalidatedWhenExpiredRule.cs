using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules
{
    internal class TokenCannotBeInvalidatedWhenExpiredRule(DateTime expiresAt): IBusinessRule
    {
        public string Message => "Token cannot be invalidated when it is expired.";
        public bool IsBroken() => DateTime.UtcNow >= expiresAt;
    }
}
