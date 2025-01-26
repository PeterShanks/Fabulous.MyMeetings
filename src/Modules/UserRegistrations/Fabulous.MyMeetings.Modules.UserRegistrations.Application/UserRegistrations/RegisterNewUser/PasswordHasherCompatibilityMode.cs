namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

public enum PasswordHasherCompatibilityMode
{
    /// <summary>
    ///     Indicates hashing passwords in a way that is compatible with ASP.NET Identity versions 1 and 2.
    /// </summary>
    IdentityV2,

    /// <summary>
    ///     Indicates hashing passwords in a way that is compatible with ASP.NET Identity version 3.
    /// </summary>
    IdentityV3
}