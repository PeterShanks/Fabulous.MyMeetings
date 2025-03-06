using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;

public class BusinessExceptionProblemDetails : ProblemDetails
{
    public BusinessExceptionProblemDetails(BusinessException exception)
    {
        Title = "Business rule broken";
        Status = StatusCodes.Status400BadRequest;
        Detail = exception.Message;
        Type = "https://fabulous.com/business-validation-error";
    }
}