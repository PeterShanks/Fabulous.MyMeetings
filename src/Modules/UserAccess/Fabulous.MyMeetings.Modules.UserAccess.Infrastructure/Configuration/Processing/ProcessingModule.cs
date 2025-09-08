using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Scrutor;
using System.Text.Json;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;

internal static class ProcessingModule
{
    public static void AddProcessing(this IServiceCollection services,
        BiDictionary<string, Type> domainNotificationsMap)
    {
        CheckMappings(domainNotificationsMap);
        services.AddSingleton(domainNotificationsMap);

        services.AddSingleton(_ => new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            AllowTrailingCommas = true
        });
        services.AddSingleton<TimeProvider>(TimeProvider.System);
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainNotificationsMapper, DomainNotificationsMapper>();
        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();
        services.AddSingleton<IDomainEventNotificationFactory, DomainEventNotificationFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddResiliencePipeline(PollyPolicies.WaitAndRetry, builder =>
        {
            builder.AddRetry(new RetryStrategyOptions
            {
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromSeconds(2),
                ShouldHandle = new PredicateBuilder()
                    .Handle<Exception>()
            });
        });

        services.AddScoped<ICommandsScheduler, CommandsScheduler>();
        services.Scan(scan => scan
            .FromAssemblies(Assemblies)
            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<>))
                        .WithoutAttribute<SkipAutoRegistrationAttribute>(),
                publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<,>))
                        .WithoutAttribute<SkipAutoRegistrationAttribute>(),
                publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes =>
                    classes.AssignableTo(typeof(IQueryHandler<,>))
                        .WithoutAttribute<SkipAutoRegistrationAttribute>(),
                publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandHandlerWithResultDecorator<,>));

        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingCommandHandlerWithResultDecorator<,>));
        services.Decorate(typeof(IRequestHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingCommandHandlerWithResultDecorator<,>));

        services.TryDecorate(typeof(INotificationHandler<>), typeof(DomainEventsDispatcherNotificationHandlerDecorator<>));
    }

    private static void CheckMappings(BiDictionary<string, Type> domainNotificationsMap)
    {
        var domainEventNotifications = Assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && t.GetInterfaces().Contains(typeof(IDomainEventNotification)));

        var unmappedNotifications = new List<Type>();

        foreach (var domainEventNotification in domainEventNotifications)
            if (!domainNotificationsMap.TryGetBySecond(domainEventNotification, out var name))
                unmappedNotifications.Add(domainEventNotification);

        if (unmappedNotifications.Count > 0)
        {
            var unmappedNotificationNames = unmappedNotifications
                .Select(x => x.FullName)
                .Aggregate((acc, cur) => string.Join(',', acc, cur));

            throw new ApplicationException($"Domain Event Notifications {unmappedNotificationNames} are not mapped");
        }
    }
}