// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.NotificationSystem.Internal;
using TTShang.Core.Api.Impl.NotificationSystem.Services;
using TTShang.Core.Module;
using TTShang.Core.NotificationSystem;

namespace TTShang.Core.Api.Impl.NotificationSystem
{
    /// <summary>
    /// 模块
    /// </summary>
    public class NotificationSystemServerModule : NotificationSystemModule, IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

        /// <summary>
        /// 限定模块下Api ControlType
        /// </summary>
        /// <remarks>
        /// 如果同一<see cref="ApiGroupName"/>下有多个模块，请限制要自动写入function的api的ControlType,否则将出现重复扫描。
        /// </remarks>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return [typeof(UserConnectQueryService)];
            }
        }
    }
}
