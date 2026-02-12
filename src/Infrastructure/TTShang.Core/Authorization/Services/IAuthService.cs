// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Authorization.Services
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity? GetIdentity();
        /// <summary>
        /// 获取当前请求的功能点
        /// </summary>
        /// <returns></returns>
        Task<ApiEndpoint?> GetApiEndpoint();
        /// <summary>
        /// 检测身份可用性
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckIdentityUsability();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> ChecktContenxtApiEndpoint();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object? GetIdentityId();

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns></returns>
        Task<bool> IsSuperAdministrator();

        /// <summary>
        /// 判断当前登录对象是否有该资源
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        Task<bool> CheckCurrentIdentityHaveResource(string resourceKey);
    }
}