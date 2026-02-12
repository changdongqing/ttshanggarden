// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.SystemAsset.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService : IServiceBase<ResourceDto, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据父级编号查询所有子级资源
        /// </remarks>
        /// <param name="id">父id</param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetChildren(Guid id);

        /// <summary>
        /// 查询根节点
        /// </summary>
        /// <remarks>
        /// 查询所有根节点
        /// </remarks>
        /// <returns></returns>
        Task<List<ResourceDto>> GetRoot();

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 查询所有资源,按树形结构返回
        /// <para>
        /// 非租户在所有资源中抽取，租户在自己的资源池中抽取
        /// </para>
        /// </remarks>
        /// <param name="includLocked">是否包含锁定的资源</param>
        /// <param name="rootKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="supportMultiTenant">是否支持多租户</param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetTree(bool includLocked = true, string? rootKey = null, Guid? tenantId = null, bool? supportMultiTenant = null);

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <remarks>
        /// 根据资源id获取已绑定接口
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(Guid id);

    }
}