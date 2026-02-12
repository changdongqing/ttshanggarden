// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

namespace TTShang.Core.NotificationSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationDataJsonConverter : JsonConverter<NotificationData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override NotificationData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerCopy = reader;
            string? typeAssemblyName = null;
            while (readerCopy.Read())
            {
                if (readerCopy.TokenType == JsonTokenType.PropertyName)
                {
                    var str = readerCopy.GetString();
                    if (str != null && str.ToLower().Equals(nameof(NotificationData.TypeAssemblyName),StringComparison.OrdinalIgnoreCase))
                    {
                        readerCopy.Read();
                        if (readerCopy.TokenType == JsonTokenType.String)
                        {
                            typeAssemblyName = readerCopy.GetString();
                            break;
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(typeAssemblyName))
            {
                throw new JsonException($"{nameof(NotificationData.TypeAssemblyName)} is required");
            }
            Type? t = Type.GetType(typeAssemblyName);
            if (t == null)
            {
                throw new JsonException($"{typeAssemblyName} type is not find");
            }
            var newOptions = new JsonSerializerOptions(options);
            newOptions.PropertyNameCaseInsensitive = true;
            newOptions.IncludeFields = true;
            object? data = JsonSerializer.Deserialize(ref reader, t, newOptions);
            return data == null ? null : (NotificationData)data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, NotificationData value, JsonSerializerOptions options)
        {
            JsonSerializerOptions tempOptions = new JsonSerializerOptions(options);
            tempOptions.Converters.Clear();
            Type? t = null;
            if (!string.IsNullOrEmpty(value.TypeAssemblyName))
            {
                t = Type.GetType(value.TypeAssemblyName);
            }
            if (t == null)
            {
                t = typeof(NotificationData);
            }
            JsonSerializer.Serialize(writer, value, t, tempOptions);
        }
    }
}
