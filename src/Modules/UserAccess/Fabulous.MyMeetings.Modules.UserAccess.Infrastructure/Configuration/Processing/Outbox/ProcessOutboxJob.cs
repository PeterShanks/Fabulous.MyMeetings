using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxJob : IJob
{
    private readonly IMediator _mediator;

    public ProcessOutboxJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Execute(IJobExecutionContext context)
    {
        return _mediator.Send(new ProcessOutboxCommand());
    }
}