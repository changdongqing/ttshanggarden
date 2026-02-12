// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Settings;
using Gardener.Core.Client.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Gardener.Core.Client.Impl.SignalR
{
    /// <summary>
    /// SignalR Client构建器
    /// </summary>
    public class SignalRClientBuilder : ISignalRClientBuilder
    {
        private string _clientName = null!;
        private string _url = null!;
        private Func<Task<string?>> _accessTokenProvider = null!;
        private JsonSerializerOptions _jsonSerializerOptions = JsonSerializerOptions.Default;
        private IClientLogger _clientLogger;
        private readonly IAuthenticationStateManager _authenticationStateManager;
        private readonly IOptions<ApiSettings> _options;
        private readonly ILocalizationLocalizer _localizer;
        /// <summary>
        ///  SignalR Client构建器
        /// </summary>
        /// <param name="clientLogger"></param>
        /// <param name="authenticationStateManager"></param>
        /// <param name="options"></param>
        /// <param name="localizer"></param>
        /// <param name="jsonSerializerOptions"></param>
        public SignalRClientBuilder(IClientLogger clientLogger, IAuthenticationStateManager authenticationStateManager, IOptions<ApiSettings> options, ILocalizationLocalizer localizer)
        {
            _clientLogger = clientLogger;
            _authenticationStateManager = authenticationStateManager;
            _options = options;
            _localizer = localizer;
        }
        public ISignalRClientBuilder GetInstance()
        {
            return new SignalRClientBuilder(_clientLogger, _authenticationStateManager, _options, _localizer);
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public ISignalRClient Build()
        {
            if (_clientName == null)
            {
                throw new ArgumentNullException("clientName");
            }
            if (_url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (_accessTokenProvider == null)
            {
                _accessTokenProvider = async () =>
                 {
                     //测试token可用性
                     await _authenticationStateManager.TestToken("signalR");
                     //token
                     TokenOutput? token = await _authenticationStateManager.LoadLocalToken();
                     if (token != null)
                     {
                         return token.AccessToken;
                     }
                     throw new Exception("not get token");
                 };
            }
            if (_url.IndexOf(":") == -1)
            {
                _url = $"{_options.Value.BaseAddres}{_url}";
            }
            SignalRClient signalRClient = new SignalRClient(_clientName, _url, _clientLogger, _accessTokenProvider, _localizer);
            return signalRClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessTokenProvider"></param>
        /// <returns></returns>
        public ISignalRClientBuilder SetAccessTokenProvider(Func<Task<string?>> accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
            return this;
        }

        public ISignalRClientBuilder SetClientName(string clientName)
        {
            _clientName = clientName;
            return this;
        }

        public ISignalRClientBuilder SetLogger(IClientLogger clientLogger)
        {
            _clientLogger = clientLogger;
            return this;
        }

        public ISignalRClientBuilder SetUrl(string url)
        {
            _url = url;
            return this;
        }

        public ISignalRClientBuilder SetJsonSerializerOptions(JsonSerializerOptions options)
        {
            _jsonSerializerOptions = options;
            return this;
        }
    }
}
