// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.JsonSerialization;
using Gardener.Core.Api.Impl.Printer.Services;
using Gardener.Core.Module;
using Gardener.Core.Printer;
using Gardener.Core.Printer.Dtos;
using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Gardener.Core.Api.Impl.Printer
{
    /// <summary>
    /// 打印服务
    /// </summary>
    public static class PrinterExtensions
    {
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();

        static PrinterExtensions()
        {
            jsonSerializerOptions.Converters.Add(new PrintCommandJsonConverter());
            jsonSerializerOptions.IncludeFields = true;
        }

        /// <summary>
        /// 打印服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPrinter(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, PrinterServerModule>();
            services.AddKeyedSingleton<IPrintCommandConvert, EscPrintCommandConvert>(PrintProtocolType.ESC_POS.ToString());

            services.AddSingleton<IPrintCommandService, PrintCommandService>();
            services.AddScoped<IPrintTemplateService, PrintTemplateService>();
            services.AddScoped<IPrintService, PrintService>();

            services.AddRestController<PrintService>();
            services.AddRestController<PrintTemplateService>();
            return services;
        }

        /// <summary>
        /// 转Json
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static string ToJson(this List<PrintCommand> commands)
        {
            string json = JsonSerializer.Serialize(commands, jsonSerializerOptions);

            return json;
        }

        /// <summary>
        /// 转命令
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static List<PrintCommand>? ToCommands(this string commands)
        {
            return JsonSerializer.Deserialize<List<PrintCommand>>(commands, jsonSerializerOptions);
        }
    }
}
