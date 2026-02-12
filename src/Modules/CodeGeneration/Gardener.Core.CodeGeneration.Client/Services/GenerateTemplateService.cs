// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.CodeGeneration.Client.Services
{
    /// <summary>
    /// 生成模板服务
    /// </summary>
    [ScopedService]
    public class GenerateTemplateService : ClientServiceBase<GenerateTemplateDto, int>, IGenerateTemplateService
    {
        /// <summary>
        /// 生成模板服务
        /// </summary>
        /// <param name="apiCaller"></param>
        public GenerateTemplateService(IApiCaller apiCaller) : base(apiCaller, "generate-template", "code-gen")
        {
        }
    }
}
