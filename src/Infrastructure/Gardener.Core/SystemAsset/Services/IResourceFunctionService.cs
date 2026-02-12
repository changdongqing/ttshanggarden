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

namespace Gardener.Core.SystemAsset.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceFunctionService : IServiceBaseNoKey<ResourceFunctionDto>
    {


        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 删除资源与接口关系
        /// </remarks>
        /// <param name="resourceId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid resourceId, Guid functionId);

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <remarks>
        /// 根据资源编号获取种子数据
        /// </remarks>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        Task<string> GetSeedData(List<Guid> resourceIds);
    }
}
