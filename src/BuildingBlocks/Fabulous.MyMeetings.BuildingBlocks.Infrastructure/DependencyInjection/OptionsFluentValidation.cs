using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;

public class OptionsFluentValidation<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly string? _name;
    private readonly IServiceProvider _serviceProvider;

    public OptionsFluentValidation(string? name, IServiceProvider serviceProvider)
    {
        _name = name;
        _serviceProvider = serviceProvider;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (name != null && name != _name)
            return ValidateOptionsResult.Skip;

        // Ensure options are provided to validate against
        ArgumentNullException.ThrowIfNull(options);

        // Validators are registered as scoped, so need to create a scope,
        // as we will be called from the root scope
        using var scope = _serviceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();
        var results = validator.Validate(options);
        if (results.IsValid)
            return ValidateOptionsResult.Success;

        var typeName = options.GetType().Name;
        var errors = new List<string>();
        foreach (var result in results.Errors)
            errors.Add(
                $"Fluent validation failed for '{typeName}.{result.PropertyName}' with the error: '{result.ErrorMessage}'.");

        return ValidateOptionsResult.Fail(errors);
    }
}