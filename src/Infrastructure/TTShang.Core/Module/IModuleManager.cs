// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Module
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        IEnumerable<IModule> GetModules();

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IModule? GetModule(string name);
    }
}
