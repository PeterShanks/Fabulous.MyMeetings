using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal static class ProcessingModule
    {
        public static void AddProcessing(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<IDomainNotificationsMapper, DomainNotificationsMapper>();
            services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
            services.AddSingleton<IDomainEventNotificationFactory, DomainEventNotificationFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddResiliencePipeline(PollyPolicies.WaitAndRetry, builder =>
            {
                builder.AddRetry(new RetryStrategyOptions()
                {
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(2),
                    ShouldHandle = new PredicateBuilder()
                        .Handle<Exception>()
                });
            });
            services.AddScoped<ICommandsScheduler, ICommandsScheduler>();
            services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

            services.Decorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandHandlerWithResultDecorator<,>));

            services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingCommandHandlerWithResultDecorator<,>));

            // TODO: Is this of any use?
            services.Decorate(typeof(INotificationHandler<>), typeof(DomainEventsDispatcherNotificationHandlerDecorator<>));
        }
    }
}
