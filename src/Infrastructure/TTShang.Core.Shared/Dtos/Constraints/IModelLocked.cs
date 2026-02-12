// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Dtos.Constraints
{
    /// <summary>
    /// 锁定
    /// </summary>
    public interface IModelLocked
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IsLocked), ResourceType = typeof(SharedLocalResource))]
        public bool IsLocked { get; set; }
    }
}
