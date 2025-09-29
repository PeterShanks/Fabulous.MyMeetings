using Fabulous.MyMeetings.Modules.Administration.Application.Contracts;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure;

public class AdministrationModule(): IAdministrationModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        using (AdministrationStartup.BeginLoggerScope())
        await using (var scope = AdministrationStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }
    }

    public async Task ExecuteCommandAsync(ICommand command)
    {
        using (AdministrationStartup.BeginLoggerScope())
        await using (var scope = AdministrationStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using (AdministrationStartup.BeginLoggerScope())
        await using (var scope = AdministrationStartup.BeginScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}