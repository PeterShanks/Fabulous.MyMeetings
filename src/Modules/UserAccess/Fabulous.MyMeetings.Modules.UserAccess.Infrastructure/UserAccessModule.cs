using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;

internal class UserAccessModule : IUserAccessModule
{
    public Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using var scope = CompositionRoot.BeginScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return mediator.Send(command);
    }

    public Task ExecuteCommandAsync(ICommand command)
    {
        using var scope = CompositionRoot.BeginScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return mediator.Send(command);
    }

    public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = CompositionRoot.BeginScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return mediator.Send(query);
    }
}