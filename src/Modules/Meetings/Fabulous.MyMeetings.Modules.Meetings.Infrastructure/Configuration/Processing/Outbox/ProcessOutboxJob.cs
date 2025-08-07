using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxJob(IMediator mediator) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return mediator.Send(new ProcessOutboxCommand());
    }
}