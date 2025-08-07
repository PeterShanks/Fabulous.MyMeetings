using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;

public class MeetingsModule : IMeetingsModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using (MeetingsStartup.BeginLoggerScope())
        await using (var scope = MeetingsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }
    }

    public async Task ExecuteCommandAsync(ICommand command)
    {
        using (MeetingsStartup.BeginLoggerScope())
        await using (var scope = MeetingsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using (MeetingsStartup.BeginLoggerScope())
        await using (var scope = MeetingsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}