using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(NotFoundException exception)
    {
        Title = "Resource not found";
        Status = StatusCodes.Status404NotFound;
        Type = "https://fabulous.com/errors/not-found";
        Detail = exception.Message;
    }
}