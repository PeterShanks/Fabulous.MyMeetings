using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing;

internal class LoggingCommandHandlerDecorator<TCommand>(ICommandHandler<TCommand> decorated,
    IExecutionContextAccessor executionContextAccessor, ILogger logger) : ICommandHandler<TCommand>
    where TCommand : ICommand

{
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (request is IRecurringCommand)
        {
            await decorated.Handle(request, cancellationToken);
            return;
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

                await decorated.Handle(request, cancellationToken);

                logger.LogInformation("Command {Command} processed successful", request.GetType().Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Command {Command} processing failed", request.GetType().Name);
                throw;
            }
        }
    }
}