// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Services;

namespace Gardener.Core.Api.Impl.Printer.Services
{
    /// <summary>
    /// 打印服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class PrintService : IPrintService
    {
        private readonly IPrintTemplateService printTemplateService;
        /// <summary>
        /// 打印服务
        /// </summary>
        /// <param name="printTemplateService"></param>
        public PrintService(IPrintTemplateService printTemplateService)
        {
            this.printTemplateService = printTemplateService;
        }
        /// <summary>
        /// 获取测试数据base64格式
        /// </summary>
        /// <param name="protocolType"></param>
        /// <returns></returns>
        public  Task<string?> GetTestDataBase64(PrintProtocolType protocolType)
        {
            return printTemplateService.ConvertToBase64("test_template", protocolType, DateTime.Now);
        }
        
    }
}
