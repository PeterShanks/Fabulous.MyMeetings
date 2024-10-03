using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public class TypedIdValueConverter<T>(ConverterMappingHints? mappingHints = null) : ValueConverter<T, Guid>(id => id.Value, value => Create(value), mappingHints)
    where T : TypedId
{
    private static T Create(Guid id)
    {
        return Activator.CreateInstance(typeof(T), id) as T ??
               throw new InvalidOperationException(
                   $"TypedIdValueGenerator: Couldn't create instance of type {typeof(T).FullName}");
    }
}