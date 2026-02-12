// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Pages.ClientView
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientFunctionEditOption
    {
        /// <summary>
        /// 选中的id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 0 展示
        /// 1 添加
        /// </summary>
        public int Type { get; set; }
    }
}
