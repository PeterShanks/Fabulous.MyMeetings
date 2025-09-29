using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Concurrent;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public class TypedIdValueConverter<T>(ConverterMappingHints? mappingHints = null) 
    : ValueConverter<T, Guid>(id => id.Value, value => Create(value), mappingHints)
    where T : TypedId
{
    private static T Create(Guid id)
    {
        return Activator.CreateInstance(typeof(T), id) as T ??
               throw new InvalidOperationException(
                   $"TypedIdValueGenerator: Couldn't create instance of type {typeof(T).FullName}");
    }
}

public class TypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
    : ValueConverterSelector(dependencies)
{
    private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters = [];
    private static readonly Type TypedIdType = typeof(TypedId);
    private static readonly Type TypedIdValueConverterType = typeof(TypedIdValueConverter<>);

    public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
    {
        var baseConverters = base.Select(modelClrType, providerClrType);
        foreach (var converter in baseConverters)
        {
            yield return converter;
        }

        // Extract the "real" type T from Nullable<T> if required
        var underlyingModelType = UnwrapNullableType(modelClrType);
        var underlyingProviderType = UnwrapNullableType(providerClrType);

        // 'null' means 'get any value converters for the modelClrType'
        if (underlyingProviderType is not null && underlyingProviderType != typeof(Guid)) 
            yield break;

        if (!TypedIdType.IsAssignableFrom(underlyingModelType)) 
            yield break;

        var converterType = TypedIdValueConverterType.MakeGenericType(underlyingModelType);

        yield return _converters.GetOrAdd(
            (underlyingModelType, typeof(Guid)),
            _ =>
            {
                return new ValueConverterInfo(
                    modelClrType, 
                    typeof(Guid),
                    valueConverterInfo => (ValueConverter)Activator.CreateInstance(converterType, valueConverterInfo.MappingHints)!);
            }
        );
    }

    private static Type? UnwrapNullableType(Type? type)
    {
        if (type is null) return null;

        return Nullable.GetUnderlyingType(type) ?? type;
    }
}