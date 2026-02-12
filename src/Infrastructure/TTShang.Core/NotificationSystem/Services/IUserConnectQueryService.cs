// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.NotificationSystem.Services
{
    /// <summary>
    /// 用户链接查询服务
    /// </summary>
    public interface IUserConnectQueryService
    {

        /// <summary>
        /// 获取用户链接
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<UserConnectionInfo>?> GetUserConnectionInfos(IdentityType identityType, string id);

        /// <summary>
        /// 获取用户在线状态
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckUserIsOnline(IdentityType identityType, string id);
    }
}
