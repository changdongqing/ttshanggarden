// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.CodeGeneration.Dtos;
using Gardener.Core.Common;

namespace Gardener.Core.CodeGeneration.Services
{
    /// <summary>
    /// 生成模板服务
    /// </summary>
    public interface IGenerateTemplateService : IServiceBase<GenerateTemplateDto, int>
    {
    }
}
