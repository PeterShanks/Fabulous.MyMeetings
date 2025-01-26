using Fabulous.MyMeetings.BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.Validation
{
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
}
