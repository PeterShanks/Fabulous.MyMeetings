using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing;

internal class ValidationCommandHandlerWithResultDecorator<TCommand, TResult>(IEnumerable<IValidator<TCommand>> validators,
    ICommandHandler<TCommand, TResult> decorated) : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var errors = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .Select(error => error.ErrorMessage)
            .ToList();

        if (errors.Count > 0)
            throw new InvalidCommandException(errors);

        return decorated.Handle(request, cancellationToken);
    }
}