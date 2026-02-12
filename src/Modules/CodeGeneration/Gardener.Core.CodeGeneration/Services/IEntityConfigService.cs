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
    /// 实体类配置服务
    /// </summary>
    public interface IEntityConfigService:IServiceBase<EntityConfigDto,string>
    {
    }
}
