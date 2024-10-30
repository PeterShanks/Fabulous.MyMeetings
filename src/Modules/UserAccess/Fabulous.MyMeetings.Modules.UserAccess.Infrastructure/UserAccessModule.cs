using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;

public class UserAccessModule : IUserAccessModule
{
    public Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using (UserAccessStartup.BeginLoggerScope())
        using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(command);
        }
    }

    public Task ExecuteCommandAsync(ICommand command)
    {
        using (UserAccessStartup.BeginLoggerScope())
        using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(command);
        }
    }

    public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using (UserAccessStartup.BeginLoggerScope())
        using (var scope = UserAccessStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(query);
        }
    }
}