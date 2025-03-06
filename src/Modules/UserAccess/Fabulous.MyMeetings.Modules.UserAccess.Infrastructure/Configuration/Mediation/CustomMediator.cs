using System.Collections.Concurrent;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using MediatR;
using MediatR.NotificationPublishers;
using MediatR.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Mediation
{
    public class CustomMediator(IServiceProvider serviceProvider, INotificationPublisher publisher)
        : IMediator
    {
        private readonly IMediator _mediator = new Mediator(serviceProvider, publisher);
        private static readonly ConcurrentDictionary<Type, RequestHandlerBase> RequestHandlers = new();

        public CustomMediator(IServiceProvider serviceProvider)
            : this(serviceProvider, new ForeachAwaitPublisher()) { }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var requestType = request.GetType();

            var handler = request is ICommand<TResponse>
                ? (RequestHandlerWrapper<TResponse>)GetHandler(requestType, typeof(CommandHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)))
                : (RequestHandlerWrapper<TResponse>)GetHandler(requestType, typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)));

            return handler.Handle(request, serviceProvider, cancellationToken);
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest
        {
            ArgumentNullException.ThrowIfNull(request);
            var requestType = request.GetType();

            var handler = request is ICommand
                ? (RequestHandlerWrapper)GetHandler(requestType, typeof(CommandHandlerWrapperImpl<>).MakeGenericType(requestType))
                : (RequestHandlerWrapper)GetHandler(requestType, typeof(RequestHandlerWrapperImpl<>).MakeGenericType(requestType));

            return handler.Handle(request, serviceProvider, cancellationToken);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            
            var handler = RequestHandlers.GetOrAdd(request.GetType(), static requestType =>
            {
                Type wrapperType;

                if (TryGetInterfaceImplementation(requestType, typeof(ICommand<>), out Type[] types ))
                    wrapperType = typeof(CommandHandlerWrapperImpl<,>).MakeGenericType(requestType, types[0]);
                else if (TryGetInterfaceImplementation(requestType, typeof(ICommand), out _))
                    wrapperType = typeof(CommandHandlerWrapperImpl<>).MakeGenericType(requestType);
                else if (TryGetInterfaceImplementation(requestType, typeof(IRequest<>), out types))
                    wrapperType = typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, types[0]);
                else if (TryGetInterfaceImplementation(requestType, typeof(IRequest), out _))
                    wrapperType = typeof(RequestHandlerWrapperImpl<>).MakeGenericType(requestType);
                else
                    throw new ArgumentException($"{requestType.Name} does not implement {nameof(IRequest)}", nameof(request));

                var wrapper = Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException($"Could not create wrapper for type {requestType}");
                return (RequestHandlerBase)wrapper;
            });


            // call via dynamic dispatch to avoid calling through reflection for performance reasons
            return handler.Handle(request, serviceProvider, cancellationToken);
        }

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return _mediator.CreateStream(request, cancellationToken);
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new CancellationToken())
        {
            return _mediator.CreateStream(request, cancellationToken);
        }

        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
        {
            return _mediator.Publish(notification, cancellationToken);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
        {
            return _mediator.Publish(notification, cancellationToken);
        }

        private static RequestHandlerBase GetHandler(Type requestType, Type wrapperType)
        {
            return RequestHandlers.GetOrAdd(requestType, requestedType =>
            {
                var wrapper = Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException($"Could not create wrapper type for {requestType}");
                return (RequestHandlerBase)wrapper;
            });
        }

        private static bool TryGetInterfaceImplementation(Type type, Type interfaceType, out Type[] genericArguments)
        {
            var interfaces = type.GetInterfaces();
            var isGenericType = interfaceType.IsGenericType;

            foreach (var i in interfaces)
            {
                if (isGenericType)
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                    {
                        genericArguments = i.GetGenericArguments();
                        return true;
                    }
                }
                else if (i == interfaceType)
                {
                    genericArguments = [];
                    return true;
                }
            }

            genericArguments = [];
            return false;
        }
    }

    public class CommandHandlerWrapperImpl<TCommand> : RequestHandlerWrapperImpl<TCommand>
        where TCommand : ICommand
    {
        public override Task<Unit> Handle(IRequest request, IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            async Task<Unit> Handler()
            {
                await serviceProvider.GetRequiredService<ICommandHandler<TCommand>>()
                    .Handle((TCommand)request, cancellationToken);

                return Unit.Value;
            }

            return serviceProvider
                .GetServices<IPipelineBehavior<TCommand, Unit>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<Unit>)Handler,
                    (next, pipeline) => () => pipeline.Handle((TCommand)request, next, cancellationToken))();
        }
    }

    public class CommandHandlerWrapperImpl<TCommand, TResponse> : RequestHandlerWrapperImpl<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public override Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            Task<TResponse> Handler() => serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>()
                .Handle((TCommand)request, cancellationToken);

            return serviceProvider
                .GetServices<IPipelineBehavior<TCommand, TResponse>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<TResponse>)Handler,
                    (next, pipeline) => () => pipeline.Handle((TCommand)request, next, cancellationToken))();
        }
    }
}
