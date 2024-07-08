using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;

[DisallowConcurrentExecution]
public class ProcessInternalCommandsJob : IJob
{
    private readonly IMediator _mediator;

    public ProcessInternalCommandsJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Execute(IJobExecutionContext context)
    {
        return _mediator.Send(new ProcessInternalCommandsCommand());
    }
}