// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TTShang.Core.Printer
{
    /// <summary>
    /// 打印指令多态json处理
    /// </summary>
    public class PrintCommandJsonConverter : JsonConverter<PrintCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override PrintCommand? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerCopy = reader;
            string? typeAssemblyName = null;
            while (readerCopy.Read())
            {
                if (readerCopy.TokenType == JsonTokenType.PropertyName)
                {
                    var str = readerCopy.GetString();
                    if (str != null && str.ToLower().Equals("typeassemblyname"))
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
                throw new JsonException($"{nameof(PrintCommand.TypeAssemblyName)} is required");
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
            if(data==null)
            {
                return null;
            }
            return (PrintCommand)data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, PrintCommand value, JsonSerializerOptions options)
        {
            Type type = typeof(PrintCommand);
            if (value.TypeAssemblyName != null)
            {
                type = Type.GetType(value.TypeAssemblyName) ?? type;
            }
            JsonSerializerOptions temp = new JsonSerializerOptions(options);
            temp.Converters.Clear();
            JsonSerializer.Serialize(writer, value, type, temp);
        }
    }
}
