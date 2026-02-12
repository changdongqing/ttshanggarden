// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using TTShang.Core.Printer.Enums;
using TTShang.Core.Printer.Services;

namespace TTShang.Core.Client.Impl.Printer.Services
{
    /// <summary>
    ///  打印模板服务
    /// </summary>
    [ScopedService]
    public class PrintTemplateService : ClientServiceBase<PrintTemplateDto, Guid>, IPrintTemplateService
    {
        /// <summary>
        ///  打印模板服务
        /// </summary>
        public PrintTemplateService(IApiCaller apiCaller) : base(apiCaller, "print-template")
        {
        }

        public Task<List<PrintCommand>?> Convert(string key, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            throw new NotImplementedException();
        }

        public Task<string?> ConvertToBase64(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ConvertToByte(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            throw new NotImplementedException();
        }

        public Task<PrintTemplateDto?> GetTemplateByKey(string key)
        {
            return apiCaller.GetAsync<PrintTemplateDto?>($"{baseUrl}/template-key/{key}");
        }
    }
}
