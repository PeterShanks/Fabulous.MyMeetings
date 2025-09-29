using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration;

public class UserRegistrationsModule : IUserRegistrationsModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using (UserRegistrationsStartup.BeginLoggerScope())
        await using (var scope = UserRegistrationsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }
    }

    public async Task ExecuteCommandAsync(ICommand command)
    {
        using (UserRegistrationsStartup.BeginLoggerScope())
        await using (var scope = UserRegistrationsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using (UserRegistrationsStartup.BeginLoggerScope())
        await using (var scope = UserRegistrationsStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}