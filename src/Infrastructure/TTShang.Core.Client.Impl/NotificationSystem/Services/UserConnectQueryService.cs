// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.NotificationSystem;
using TTShang.Core.NotificationSystem.Services;

namespace TTShang.Core.Client.Impl.NotificationSystem.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class UserConnectQueryService : ClientServiceCaller, IUserConnectQueryService
    {
        public UserConnectQueryService(IApiCaller apiCaller) : base(apiCaller, "user-connect-query")
        {
        }

        public Task<bool> CheckUserIsOnline(IdentityType identityType, string id)
        {
            return apiCaller.GetAsync<bool>($"{baseUrl}/check-user-is-online/{identityType}?{nameof(id)}={id}");
        }

        public Task<List<UserConnectionInfo>?> GetUserConnectionInfos(IdentityType identityType, string id)
        {
            return apiCaller.GetAsync<List<UserConnectionInfo>?>($"{baseUrl}/user-connection-infos/{identityType}?{nameof(id)}={id}");
        }
    }
}
