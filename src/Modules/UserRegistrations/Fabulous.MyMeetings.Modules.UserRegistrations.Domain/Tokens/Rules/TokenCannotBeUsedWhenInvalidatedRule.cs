using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules
{
    internal class TokenCannotBeUsedWhenInvalidatedRule(bool isInvalidated): IBusinessRule
    {
        public string Message => "Token cannot be used when invalided";
        public bool IsBroken() => isInvalidated;
    }
}
