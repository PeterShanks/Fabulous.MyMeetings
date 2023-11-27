namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

public enum PasswordVerificationResult
{
    Failed,
    Success,
    SuccessRehashNeeded
}