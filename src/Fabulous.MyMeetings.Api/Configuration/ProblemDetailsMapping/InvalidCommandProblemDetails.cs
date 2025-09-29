using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;

public class InvalidCommandProblemDetails: ProblemDetails
{
    public List<string> Errors { get; }

    public InvalidCommandProblemDetails(InvalidCommandException exception)
    {
        Title = "Command validation error";
        Status = StatusCodes.Status400BadRequest;
        Type = "https://fabulous.com/commands/validation-error";
        Errors = exception.Errors;
    }
}