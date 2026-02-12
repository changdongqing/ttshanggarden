// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos.Constraints;
using Gardener.Core.SystemConfig.Dtos;

namespace Gardener.Core.Api.Impl.SystemConfig.Entities
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigValue : SystemConfigValueDto, IEntityBase, IModelModule
    {
    }
}
