// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using TTShang.Core.Printer.Enums;
using TTShang.Core.Printer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.Api.Impl.Printer.Services
{
    /// <summary>
    /// 打印指令
    /// </summary>
    public class PrintCommandService : IPrintCommandService
    {
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public PrintCommandService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public byte[] Convert(IReadOnlyList<PrintCommand> commands, PrintProtocolType targetType)
        {
            IPrintCommandConvert commandConvert = serviceProvider.GetRequiredKeyedService<IPrintCommandConvert>(targetType.ToString());

            return commandConvert.ConvertToByte(commands);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ConvertToBase64(IReadOnlyList<PrintCommand> commands, PrintProtocolType targetType)
        {
            return System.Convert.ToBase64String(Convert(commands, targetType));
        }
    }
}
