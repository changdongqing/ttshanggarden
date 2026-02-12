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
    /// 字段配置服务
    /// </summary>
    public interface IFieldConfigService : IServiceBase<FieldConfigDto, int>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据实体类完整名称查询
        /// </remarks>
        /// <param name="entityTypeFullName"></param>
        /// <returns></returns>
        Task<List<FieldConfigDto>> FindByEntityTypeFullName(string entityTypeFullName);
    }
}
