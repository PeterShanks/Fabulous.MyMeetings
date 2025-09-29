using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
internal class LoggingCommandHandlerDecorator<TCommand>(ICommandHandler<TCommand> decorated,
    IExecutionContextAccessor executionContextAccessor, ILogger<LoggingCommandHandlerDecorator<TCommand>> logger) : ICommandHandler<TCommand>
    where TCommand : ICommand

{
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (request is IRecurringCommand)
        {
            await decorated.Handle(request, cancellationToken);
            return;
        }


        using var _ = BeginScope(request.Id);

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

    private IDisposable BeginScope(Guid contextId)
    {
        var state = new List<KeyValuePair<string, object>> { new("Context", contextId) };

        if (executionContextAccessor.IsAvailable)
        {
            state.Add(new KeyValuePair<string, object>("CorrelationId", executionContextAccessor.CorrelationId));
        }

        return logger.BeginScope(state)!;
    }
}