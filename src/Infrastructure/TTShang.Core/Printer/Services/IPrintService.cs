// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.Printer.Services
{
    /// <summary>
    /// 打印服务
    /// </summary>
    public interface IPrintService
    {
        /// <summary>
        /// 获取测试数据base64格式
        /// </summary>
        /// <param name="protocolType"></param>
        /// <returns></returns>
        Task<string?> GetTestDataBase64(PrintProtocolType protocolType);
    }
}
