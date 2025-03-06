using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

public class UserRegistrationStatus : ValueObject
{
    private UserRegistrationStatus(string value)
    {
        Value = value;
    }

    public static UserRegistrationStatus WaitingForConfirmation => new(nameof(WaitingForConfirmation));

    public static UserRegistrationStatus Confirmed => new(nameof(Confirmed));

    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }
}