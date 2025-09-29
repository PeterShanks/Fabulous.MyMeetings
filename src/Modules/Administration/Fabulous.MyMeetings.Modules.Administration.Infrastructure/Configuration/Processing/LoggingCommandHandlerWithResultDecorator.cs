using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Administration.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
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

        using var _ = BeginScope(request.Id);
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