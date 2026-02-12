// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Services;

namespace Gardener.Core.Client.Impl.Printer.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class PrintService : ClientServiceCaller, IPrintService
    {
        public PrintService(IApiCaller apiCaller) : base(apiCaller, "print")
        {
        }
        public Task<string?> GetTestDataBase64(PrintProtocolType protocolType)
        {
            return apiCaller.GetAsync<string?>($"{baseUrl}/test-data-base64/{protocolType}");
        }
    }
}
