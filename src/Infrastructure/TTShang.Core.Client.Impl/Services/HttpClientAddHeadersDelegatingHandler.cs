// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;

namespace TTShang.Core.Client.Impl.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpClientAddHeadersDelegatingHandler : DelegatingHandler
    {
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        public HttpClientAddHeadersDelegatingHandler(IServiceProvider serviceProvider) : base(new HttpClientHandler())
        {
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //设置Culture 接口将响应本地语言
            string culture = CultureInfo.CurrentCulture.Name;
            request.Headers.Add("Accept-Language", culture);

            IAuthenticationStateManager authenticationStateManager= serviceProvider.GetRequiredService<IAuthenticationStateManager>();
            var tokenOutput=authenticationStateManager.GetCurrentToken();
            if (tokenOutput != null)
            {
                //重写token
                request.Headers.Authorization = new AuthenticationHeaderValue(GardenerAuthenticationSchemes.User, tokenOutput.AccessToken);
            }

            HttpResponseMessage httpResponse = await base.SendAsync(request, cancellationToken);

            if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                //刷新token
                tokenOutput = await authenticationStateManager.RefreshToken(true);
                if (tokenOutput != null)
                {
                    httpResponse.Dispose();
                    //重写token
                    request.Headers.Authorization = new AuthenticationHeaderValue(GardenerAuthenticationSchemes.User, tokenOutput?.AccessToken);
                    //再次请求
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            return httpResponse;
        }
    }
}
