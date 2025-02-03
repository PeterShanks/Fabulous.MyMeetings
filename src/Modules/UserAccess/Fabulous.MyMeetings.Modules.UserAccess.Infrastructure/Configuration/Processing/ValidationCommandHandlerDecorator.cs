using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;

internal class ValidationCommandHandlerDecorator<T>(IEnumerable<IValidator<T>> validators, ICommandHandler<T> decorated) : ICommandHandler<T>
    where T : ICommand
{
    public Task Handle(T request, CancellationToken cancellationToken)
    {
        var errors = validators
            .Select(v => v.Validate(request))
            .SelectMany(r => r.Errors)
            .Where(error => error is not null)
            .Select(error => error.ErrorMessage)
            .ToList();

        if (errors.Count > 0)
            throw new InvalidCommandException(errors);

        return decorated.Handle(request, cancellationToken);
    }
}