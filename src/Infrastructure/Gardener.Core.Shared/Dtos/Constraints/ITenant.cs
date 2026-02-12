// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Dtos.Constraints
{
    /// <summary>
    /// 租户基础字段
    /// </summary>
    public interface ITenant : IModelId<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Name), ResourceType = typeof(SharedLocalResource))]
        public string Name { get; set; }
    }
}
