// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Printer.Entities;
using Gardener.Core.Printer.Dtos;
using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Services;
using Microsoft.Extensions.Logging;

namespace Gardener.Core.Api.Impl.Printer.Services
{
    /// <summary>
    /// 打印模板服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class PrintTemplateService : ServiceBase<PrintTemplate, PrintTemplateDto, Guid, GardenerMultiTenantDbContextLocator>, IPrintTemplateService
    {
        private readonly ILogger<PrintTemplateService> _logger;
        private readonly IViewEngine viewEngine;
        private readonly IPrintCommandService convertService;
        /// <summary>
        /// 打印模板服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="viewEngine"></param>
        /// <param name="convertService"></param>
        /// <param name="logger"></param>
        public PrintTemplateService(IRepository<PrintTemplate, GardenerMultiTenantDbContextLocator> repository, IViewEngine viewEngine, IPrintCommandService convertService, ILogger<PrintTemplateService> logger) : base(repository)
        {
            this.viewEngine = viewEngine;
            this.convertService = convertService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<List<PrintCommand>?> Convert(string key, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            var template = await GetTemplateByKey(key);
            if (template == null)
            {
                return null;
            }
            try
            {
                string cacheKey = template.TemplateKey + (template.UpdatedTime.HasValue ? template.UpdatedTime.Value.ToString("yyyyMMddHHmmss") : "");
                string result = await viewEngine.RunCompileFromCachedAsync(template.TemplateContent, model, cacheKey, builderAction: builder =>
                {
                    builder.AddAssemblyReferenceByName("Gardener.Core");
                    builder.AddAssemblyReferenceByName("Gardener.Core.Util");
                    builder.AddAssemblyReferenceByName("Gardener.Core.Api.Impl");
                    if (assemblyNames != null)
                    {
                        foreach (var item in assemblyNames)
                        {
                            builder.AddAssemblyReferenceByName(item);
                        }
                    }
                    builder.AddUsing("System");
                    builder.AddUsing("Gardener.Core.Util");
                    builder.AddUsing("Gardener.Core.Printer.Dtos");
                    builder.AddUsing("Gardener.Core.Printer.Enums");
                    builder.AddUsing("Gardener.Core.Api.Impl.Printer");
                    foreach (var name in usingNames)
                    {
                        builder.AddUsing(name);
                    }
                });
                if (TemplateResultType.CommandJson.Equals(template.TemplateResultType))
                {
                    return result.ToCommands();
                }
                else
                {
                    return [new PrintTextCommand(result)];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"模板编译错误 key:{key}");
                throw Oops.Bah(SharedLocalResource.Script_Code_Compile_Fail);
            }
        }
        /// <summary>
        /// 转换成对应协议的Base64字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="protocolType"></param>
        /// <param name="model"></param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<string?> ConvertToBase64(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            List<PrintCommand>? commandList = await Convert(key, model, assemblyNames, usingNames);
            if (commandList == null)
            {
                return null;
            }
            return convertService.ConvertToBase64(commandList, protocolType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="protocolType"></param>
        /// <param name="model"></param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<byte[]> ConvertToByte(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames)
        {
            List<PrintCommand>? commandList = await Convert(key, model, assemblyNames, usingNames);
            if (commandList == null)
            {
                return [];
            }
            return convertService.Convert(commandList, protocolType);
        }


        /// <summary>
        /// 获取模板
        /// </summary>
        /// <remarks>
        /// 根据模板键获取模板
        /// </remarks>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<PrintTemplateDto?> GetTemplateByKey(string key)
        {
            return _repository.AsQueryable(false).Where(x => x.TemplateKey.Equals(key)).Select(x => x.Adapt<PrintTemplateDto>()).FirstOrDefaultAsync();
        }
    }
}
