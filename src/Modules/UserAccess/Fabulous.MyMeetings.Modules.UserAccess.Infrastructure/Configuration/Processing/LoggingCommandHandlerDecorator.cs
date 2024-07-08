using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;

internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand

{
    private readonly ICommandHandler<TCommand> _decorated;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly ILogger _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decorated,
        IExecutionContextAccessor executionContextAccessor, ILogger logger)
    {
        _decorated = decorated;
        _executionContextAccessor = executionContextAccessor;
        _logger = logger;
    }

    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (request is IRecurringCommand)
            await _decorated.Handle(request, cancellationToken);

        using (_logger.BeginScope(new List<KeyValuePair<string, object>>
               {
                   new("CorrelationId", _executionContextAccessor.CorrelationId),
                   new("Context", request.Id)
               }))
        {
            try
            {
                _logger.LogInformation("Executing command {Command}", request.GetType().Name);

                await _decorated.Handle(request, cancellationToken);

                _logger.LogInformation("Command {Command} processed successful", request.GetType().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Command {Command} processing failed", request.GetType().Name);
                throw;
            }
        }
    }
}