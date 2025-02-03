using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing;

internal class LoggingCommandHandlerWithResultDecorator<TCommand, TResult>(
    ILogger<LoggingCommandHandlerWithResultDecorator<TCommand, TResult>> logger,
    IExecutionContextAccessor executionContextAccessor,
    ICommandHandler<TCommand, TResult> decorated) : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (request is IRecurringCommand)
        {
            return await decorated.Handle(request, cancellationToken);
        }

        using (logger.BeginScope(new List<KeyValuePair<string, object>>
               {
                   new("CorrelationId", executionContextAccessor.CorrelationId),
                   new("Context", request.Id)
               }))
        {
            try
            {
                logger.LogInformation("Executing command {Command}", request.GetType().Name);

                var result = await decorated.Handle(request, cancellationToken);

                logger.LogInformation("Command {Command} processed successful, result {Result}",
                    request.GetType().Name, result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Command {Command} processing failed", request.GetType().Name);
                throw;
            }
        }
    }
}