using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
        where T: ICommand
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly ICommandHandler<T> _decorated;

        public ValidationCommandHandlerDecorator(IList<IValidator<T>> validators, ICommandHandler<T> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public Task Handle(T request, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(r => r.Errors)
                .Where(error => error is not null)
                .Select(error => error.ErrorMessage)
                .ToList();

            if (errors.Count > 0)
                throw new InvalidCommandException(errors);

            return _decorated.Handle(request, cancellationToken);
        }
    }
}
