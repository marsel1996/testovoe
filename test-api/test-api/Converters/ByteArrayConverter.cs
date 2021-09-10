using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace test_api.Converters
{
    public class ByteArrayConverter : Newtonsoft.Json.JsonConverter<byte[]>
    {
        public override void WriteJson(JsonWriter writer, [AllowNull] byte[] value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert.ToBase64String(value ?? Array.Empty<byte>()));
        }

        public override byte[] ReadJson(JsonReader reader, Type objectType, [AllowNull] byte[] existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            using var m = new MemoryStream(Convert.FromBase64String(reader.Value as string ?? ""));
            return m.ToArray();
        }
    }
}
