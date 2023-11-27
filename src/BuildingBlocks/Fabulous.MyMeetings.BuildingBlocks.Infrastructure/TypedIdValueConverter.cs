using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure
{
    public class TypedIdValueConverter<T> : ValueConverter<T, Guid>
        where T : TypedId
    {
        public TypedIdValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        private static T Create(Guid id) => Activator.CreateInstance(typeof(T), id) as T ??
                                            throw new InvalidOperationException(
                                                $"TypedIdValueGenerator: Couldn't create instance of type {typeof(T).FullName}");
    }
}
