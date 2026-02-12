// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.SystemConfig
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigModule : IModule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "SystemConfig";
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description => "系统配置";


        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 240;
    }
}
