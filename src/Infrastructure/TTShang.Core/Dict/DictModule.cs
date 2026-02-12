// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Enums;
using TTShang.Core.Module;

namespace TTShang.Core.Dict
{
    /// <summary>
    /// 模块
    /// </summary>
    public class DictModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Dict";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "字典模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 160;

        /// <summary>
        /// 自定义异常code类型
        /// </summary>
        /// <returns></returns>
        public Type[]? GetCustomErrorCodeTypes()
        {
            return [typeof(DictExceptionCode)];
        }
    }
}
