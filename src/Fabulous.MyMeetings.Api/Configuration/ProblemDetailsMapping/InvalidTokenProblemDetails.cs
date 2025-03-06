using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;

public class InvalidTokenProblemDetails : ProblemDetails
{
    public InvalidTokenProblemDetails()
    {
        Title = "Invalid token";
        Status = StatusCodes.Status400BadRequest;
        Type = "https://fabulous.com/commands/invalid-token";
    }
}