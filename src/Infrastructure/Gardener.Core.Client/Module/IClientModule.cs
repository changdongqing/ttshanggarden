// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Client.Module
{
    /// <summary>
    /// 客户端模块定义
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public interface IClientModule : IModule
    {

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        Task Load(IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取自动注册的组件
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleComponent> GetAutoRegisterComponents()
        {
            return [];
        }
    }
}
