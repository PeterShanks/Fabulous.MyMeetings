using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.ProblemDetailsMapping;

public class BusinessRuleValidationExceptionProblemDetails: ProblemDetails
{
    public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        Title = "Business rule broken";
        Status = StatusCodes.Status409Conflict;
        Detail = exception.Message;
        Type = "https://fabulous.com/business-rule-validation-error";
    }
}