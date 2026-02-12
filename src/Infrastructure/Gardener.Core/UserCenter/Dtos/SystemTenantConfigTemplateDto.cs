// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos.Constraints;

namespace Gardener.Core.UserCenter.Dtos
{
    /// <summary>
    /// 租户配置模板
    /// </summary>
    public class SystemTenantConfigTemplateDto : BaseDto<int>, IModelModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string? ModuleName { set; get; }
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = null!;
        /// <summary>
        /// 默认配置值
        /// </summary>
        public string? DefaultConfigValue { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = null!;
    }
}
