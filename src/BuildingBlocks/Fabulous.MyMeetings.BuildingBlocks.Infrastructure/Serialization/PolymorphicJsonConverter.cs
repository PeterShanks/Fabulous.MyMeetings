using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Serialization
{
    public class PolymorphicJsonConverter<T>: JsonConverter<T>
        where T : class
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
                throw new JsonException("Json value is not an object");

            if(!root.TryGetProperty("$type", out var typeProperty))
                throw new JsonException("Json object does not contain $type property");

            var typeName = typeProperty.GetString();

            if (string.IsNullOrWhiteSpace(typeName))
                throw new JsonException("Value of $type property is invalid");

            var type = Type.GetType(typeName) ?? throw new JsonException($"Type {typeName} not found");

            if (!typeof(T).IsAssignableFrom(type))
                throw new JsonException($"Type {type} is not assignable to {typeof(T)}");

            return JsonSerializer.Deserialize(root.GetRawText(), type, options) as T;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            var json = JsonSerializer.SerializeToElement(value, type, options);

            using(var doc = JsonDocument.Parse(json.GetRawText()))
            using (var modifiedDoc =
                   JsonDocument.Parse($$"""{"type":"{{type.FullName}}", {{doc.RootElement.ToString()[1..]}}"""))
            {
                modifiedDoc.WriteTo(writer);
            }
        }
    }
}
