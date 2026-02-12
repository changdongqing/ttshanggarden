// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.SystemAsset.Pages.ResourceView
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceFunctionEditOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ResourceFunctionEditOption(ResourceDto resource, string name, int type)
        {
            Resource = resource;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 选中的资源
        /// </summary>
        public ResourceDto Resource { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>
        /// 0 展示
        /// 1 添加
        /// </remarks>
        public int Type { get; set; }
    }

}
