using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static T GetWithValidation<T, TValidator>(this IConfiguration configuration)
            where T : class
            where TValidator : class, IValidator<T>
        {
            var obj = configuration.Get<T>();

            if (obj == null)
                throw new InvalidOperationException($"Configuration section {typeof(T).Name} not found.");

            var validator = Activator.CreateInstance<TValidator>();

            if (validator is null)
                throw new InvalidOperationException($"Validator for type {typeof(T).Name} not found.");

            validator.ValidateAndThrow(obj);

            return obj;
        }
    }
}
