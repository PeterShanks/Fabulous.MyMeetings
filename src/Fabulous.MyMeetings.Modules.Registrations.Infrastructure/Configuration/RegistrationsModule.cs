using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration
{
    internal class RegistrationsModule : IRegistrationsModule
    {
        public Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (RegistrationsStartup.BeginLoggerScope())
            using (var scope = RegistrationsStartup.BeginScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return mediator.Send(command);
            }
        }

        public Task ExecuteCommandAsync(ICommand command)
        {
            using (RegistrationsStartup.BeginLoggerScope())
            using (var scope = RegistrationsStartup.BeginScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return mediator.Send(command);
            }
        }

        public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (RegistrationsStartup.BeginLoggerScope())
            using (var scope = RegistrationsStartup.BeginScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return mediator.Send(query);
            }
        }
    }
}
