// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Services;

namespace Gardener.WoChat.Client.Services
{
    /// <summary>
    /// im 服务
    /// </summary>
    [ScopedService]
    public class WoChatImService :ClientServiceCaller, IWoChatImService
    {

        public WoChatImService(IApiCaller apiCaller):base(apiCaller, "wo-chat-im", "wo-chat")
        {
        }

        public Task<Guid?> AddMyImSession(ImSessionAddInput input)
        {
            return apiCaller.PostAsync<ImSessionAddInput, Guid?>($"{this.baseUrl}/my-im-session", input);
        }

        public Task<bool> DisableSessionSendMessage(Guid imSessionId)
        {
            return apiCaller.PostAsync<Guid, bool>($"{this.baseUrl}/disable-session-send-message", imSessionId);
        }

        public Task<bool> EnableSessionSendMessage(Guid imSessionId)
        {
            return apiCaller.PostAsync<Guid, bool>($"{this.baseUrl}/enable-session-send-message", imSessionId);
        }

        public Task<IEnumerable<ImSessionDto>> GetImGroupSessions()
        {
            return apiCaller.GetAsync<IEnumerable<ImSessionDto>>($"{this.baseUrl}/im-group-sessions");
        }

        public Task<ImSessionDto?> GetImSession(Guid imSessionId)
        {
            return apiCaller.GetAsync<ImSessionDto?>($"{this.baseUrl}/im-session/{imSessionId}");
        }

        public Task<IEnumerable<ImSessionDto>> GetMyImSessions()
        {
            return apiCaller.GetAsync<IEnumerable<ImSessionDto>>($"{this.baseUrl}/my-im-sessions");
        }

        public Task<IEnumerable<ImSessionMessageDto>> GetMySessionMessages(Guid imSessionId, DateTimeOffset? maxDateTime = null, int pageSize = 100)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add("imSessionId", imSessionId);
            queryString.Add("maxDateTime", maxDateTime);
            queryString.Add("pageSize", pageSize);

            return apiCaller.GetAsync<IEnumerable<ImSessionMessageDto>>($"{this.baseUrl}/my-session-messages", queryString);
        }

        public Task<bool> QuitMyImSession(Guid imSessionId)
        {
            return apiCaller.PostAsync<Guid, bool>($"{this.baseUrl}/quit-my-im-session", imSessionId);
        }


        public Task<bool> SendMessage(ImSessionMessageDto message)
        {
            return apiCaller.PostAsync<ImSessionMessageDto, bool>($"{this.baseUrl}/send-message", message);
        }

        public Task<bool> SendMessage(ImSessionMessageDto message, Identity identity)
        {
            throw new NotImplementedException();
        }
    }
}
