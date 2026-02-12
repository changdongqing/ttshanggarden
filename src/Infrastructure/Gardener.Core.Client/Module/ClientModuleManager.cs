// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using System.Reflection;

namespace Gardener.Core.Client.Module
{
    /// <summary>
    /// 客户端模块对象
    /// </summary>
    public class ClientModuleManager: IModuleManager
    {
        /// <summary>
        /// 模块Assembly集合
        /// </summary>
        private IEnumerable<Assembly> modeuleAssemblies;
        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<IClientModule> modules;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modeuleAssemblies"></param>
        /// <param name="modules"></param>
        public ClientModuleManager(IEnumerable<Assembly> modeuleAssemblies, IEnumerable<IClientModule> modules)
        {
            this.modeuleAssemblies = modeuleAssemblies;
            this.modules= modules;
        }
        /// <summary>
        /// 模块Assembly集合
        /// </summary>
        public Assembly[] ModeuleAssemblies
        {
            get
            {
                return modeuleAssemblies.ToArray();
            }
        }
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IModule> GetModules()
        {
            return modules.Select(x=>(IModule)x).OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();
        }
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IModule? GetModule(string name)
        {
            return modules.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }
    }
}
