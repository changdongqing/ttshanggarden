// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.WoChat.Impl.Entities;

namespace Gardener.WoChat.Impl.Core
{
    /// <summary>
    /// 将WoChat用户进行分组
    /// </summary>
    public class WoChatImSystemNotificationHubGrouper : ISystemNotificationHubGrouper,IScoped
    {

        private readonly IRepository<ImUserSession> imUserSessionRepository;
        /// <summary>
        /// 将WoChat用户进行分组
        /// </summary>
        /// <param name="imUserSessionRepository"></param>
        public WoChatImSystemNotificationHubGrouper(IRepository<ImUserSession> imUserSessionRepository)
        {
            this.imUserSessionRepository = imUserSessionRepository;
        }
        /// <summary>
        /// 将WoChat用户进行分组
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetGroupName(Identity identity)
        {
            //目前仅支持用户
            if (!IdentityType.User.Equals(identity.IdentityType))
            {
                return new string[0];
            }
            int userId = int.Parse(identity.Id);
            var userSessions = await imUserSessionRepository.AsQueryable(false)
                 .Where(x => x.UserId == userId).ToListAsync();

            return userSessions.Select(x => WoChatUtil.GetImGroupName(x.ImSessionId));
        }
    }
}
