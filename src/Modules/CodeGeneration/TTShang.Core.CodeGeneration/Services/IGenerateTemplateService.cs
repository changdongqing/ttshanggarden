// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.CodeGeneration.Dtos;
using TTShang.Core.Common;

namespace TTShang.Core.CodeGeneration.Services
{
    /// <summary>
    /// 生成模板服务
    /// </summary>
    public interface IGenerateTemplateService : IServiceBase<GenerateTemplateDto, int>
    {
    }
}
