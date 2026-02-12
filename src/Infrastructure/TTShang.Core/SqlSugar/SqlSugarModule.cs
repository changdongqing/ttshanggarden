// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.SqlSugar
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SqlSugarModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "SqlSugar";

        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "SqlSugar模块";
    }
}
