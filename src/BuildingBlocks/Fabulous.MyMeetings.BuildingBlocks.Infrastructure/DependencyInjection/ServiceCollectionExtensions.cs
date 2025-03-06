using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsWithValidation<TOptions, TOptionsValidator>(
        this IServiceCollection services, Action<TOptions> configuration)
        where TOptions : class
        where TOptionsValidator : class, IValidator<TOptions>
    {
        services.AddScoped<IValidator<TOptions>, TOptionsValidator>();

        services.AddOptions<TOptions>()
            .Configure(configuration)
            .ValidateUsingFluentValidation()
            .ValidateOnStart();

        services.AddSingleton<TOptions>(provider => provider.GetRequiredService<IOptions<TOptions>>().Value);

        return services;
    }
    public static IServiceCollection AddOptionsWithValidation<TOptions, TOptionsValidator>(
        this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
        where TOptionsValidator : class, IValidator<TOptions>
    {
        services.AddScoped<IValidator<TOptions>, TOptionsValidator>();

        services.AddOptions<TOptions>()
            .Bind(configuration)
            .ValidateUsingFluentValidation()
            .ValidateOnStart();

        services.AddSingleton<TOptions>(provider => provider.GetRequiredService<IOptions<TOptions>>().Value);

        return services;
    }

    private static OptionsBuilder<TOptions> ValidateUsingFluentValidation<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            provider => new OptionsFluentValidation<TOptions>(optionsBuilder.Name, provider)
        );

        return optionsBuilder;
    }
}