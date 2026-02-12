// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Dtos.Constraints
{
    /// <summary>
    /// 删除
    /// </summary>
    public interface IModelDeleted
    {
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [DisabledSearchField]
        public bool IsDeleted { get; set; }
    }
}
