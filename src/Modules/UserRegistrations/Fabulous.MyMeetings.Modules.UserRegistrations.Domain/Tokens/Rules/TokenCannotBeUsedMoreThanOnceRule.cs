using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules
{
    internal class TokenCannotBeUsedMoreThanOnceRule(bool isUsed): IBusinessRule
    {
        public string Message => "Token cannot be used more than once.";
        public bool IsBroken() => isUsed;
    }
}
