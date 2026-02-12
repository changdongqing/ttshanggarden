// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos;
using TTShang.Core.NotificationSystem;
using TTShang.Core.NotificationSystem.Services;

namespace TTShang.Core.Api.Impl.NotificationSystem.Services
{
    /// <summary>
    /// 用户链接查询服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class UserConnectQueryService : IUserConnectQueryService
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public UserConnectQueryService(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 获取用户链接
        /// </summary>
        /// <remarks>
        /// 获取用户链接
        /// </remarks>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<UserConnectionInfo>?> GetUserConnectionInfos(IdentityType identityType, [FromQuery] string id)
        {
            return systemNotificationService.GetUserConnectionInfos(identityType, id);
        }

        /// <summary>
        /// 获取用户在线状态
        /// </summary>
        /// <remarks>
        /// 获取用户在线状态
        /// </remarks>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> CheckUserIsOnline(IdentityType identityType, [FromQuery] string id)
        {
            return systemNotificationService.CheckUserIsOnline(identityType,id);
        }
    }
}
