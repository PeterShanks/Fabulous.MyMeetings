using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerWithResultDecorator<TCommand, TResult>: ICommandHandler<TCommand, TResult>
        where TCommand: ICommand<TResult>
    {
        private readonly IList<IValidator<TCommand>> _validators;

        private readonly ICommandHandler<TCommand, TResult> _decorated;

        public ValidationCommandHandlerWithResultDecorator(IList<IValidator<TCommand>> validators, ICommandHandler<TCommand, TResult> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Select(error => error.ErrorMessage)
                .ToList();

            if (errors.Count > 0)
                throw new InvalidCommandException(errors);

            return _decorated.Handle(request, cancellationToken);
        }
    }
}
