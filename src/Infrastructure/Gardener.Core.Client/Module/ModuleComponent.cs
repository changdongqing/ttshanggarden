// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Module
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="selector"></param>
        public ModuleComponent(Type component, string selector)
        {
            Component = component;
            Selector = selector;
        }


        /// <summary>
        /// 
        /// </summary>
        public Type Component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Selector { get; set; }
    }
}
