using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class LoggingCommandHandlerWithResultDecorator<TCommand, TResult>: ICommandHandler<TCommand, TResult>
        where TCommand: ICommand<TResult>
    {
        private readonly ILogger _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ICommandHandler<TCommand, TResult> _decorated;

        public LoggingCommandHandlerWithResultDecorator(
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            ICommandHandler<TCommand, TResult> decorated)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
            _decorated = decorated;
        }
        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>>()
                   {
                       new("CorrelationId", _executionContextAccessor.CorrelationId),
                       new("Context", request.Id)
                   }))
            {
                try
                {
                    _logger.LogInformation("Executing command {Command}", request.GetType().Name);

                    var result = await _decorated.Handle(request, cancellationToken);

                    _logger.LogInformation("Command {Command} processed successful, result {Result}", request.GetType().Name, result);

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Command {Command} processing failed", request.GetType().Name);
                    throw;
                }
            }
        }
    }
}
