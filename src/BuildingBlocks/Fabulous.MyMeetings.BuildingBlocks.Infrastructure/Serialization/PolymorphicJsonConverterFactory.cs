using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Serialization
{
    public class PolymorphicJsonConverterFactory(params HashSet<Type> supportedTypes): JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (supportedTypes.Contains(typeToConvert))
                return true;

            if (typeToConvert.IsGenericType && supportedTypes.Contains(typeToConvert.GetGenericTypeDefinition()))
                return true;

            return false;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (supportedTypes.Contains(typeToConvert))
            {
                var converterType = typeof(PolymorphicJsonConverter<>).MakeGenericType(typeToConvert);
                return Activator.CreateInstance(converterType) as JsonConverter;
            }

            if (typeToConvert.IsGenericType)
            {
                var genericDefinition = typeToConvert.GetGenericTypeDefinition();
                if (supportedTypes.Contains(genericDefinition))
                {
                    var converterType = typeof(PolymorphicJsonConverter<>).MakeGenericType(typeToConvert);
                    return Activator.CreateInstance(converterType) as JsonConverter;
                }
            }

            throw new NotSupportedException($"Converter for type {typeToConvert} not found");
        }
    }
}
