// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.SystemConfig.Services;
using TTShang.Core.Module;
using TTShang.Core.SystemConfig;

namespace TTShang.Core.Api.Impl.SystemConfig
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SystemConfigServerModule : SystemConfigModule, IServerModule
    {
        /// <summary>
        /// 接口分组名称-默认和模块名一致
        /// </summary>
        public string ApiGroupName
        {
            get
            {
                return nameof(Constant.InfrastructureService);
            }
        }
        /// <summary>
        /// 限定ApiTag
        /// </summary>
        /// <remarks>
        /// 如果同一<see cref="ApiGroupName"/>下有多个模块，请限制要自动注册的api的tag,否则将出现重复扫描。
        /// </remarks>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return [typeof(SystemConfigValueService)];
            }
        }
    }
}
