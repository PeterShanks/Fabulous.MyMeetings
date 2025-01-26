using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;

public class UserAccessModule : IUserAccessModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using (UserAccessStartup.BeginLoggerScope())
        await using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }
    }

    public async Task ExecuteCommandAsync(ICommand command)
    {
        using (UserAccessStartup.BeginLoggerScope())
        await using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using (UserAccessStartup.BeginLoggerScope())
        await using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}