// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization.Dtos;
using TTShang.Core.Client.Authorization;
using System.Text.Json;

namespace TTShang.Client.Auto.Entry
{
    /// <summary>
    /// wasm 登录数据存取器
    /// </summary>
    public class ServerLoginDataAccessor : ILoginDataAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ServerLoginDataAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }


        public Task<(bool, TokenOutput?)> Get()
        {
            if (httpContextAccessor.HttpContext == null)
            {
                return Task.FromResult<(bool, TokenOutput?)>((false, null));
            }
            httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("isAutoLogin", out string? isAutoLoginStr);
            //自动登录
            bool isAutoLogin = string.IsNullOrEmpty(isAutoLoginStr) ? false : bool.Parse(isAutoLoginStr);
            //token
            httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(nameof(TokenOutput), out string? tokenStr);
            if (tokenStr == null)
            {
                return Task.FromResult<(bool, TokenOutput?)>((isAutoLogin, null));
            }
            TokenOutput? tokenOutput = JsonSerializer.Deserialize<TokenOutput>(tokenStr);
            return Task.FromResult<(bool, TokenOutput?)>((isAutoLogin, tokenOutput));
        }

        public Task Remove(bool isAutoLogin)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                //clear
                httpContextAccessor.HttpContext.Response.Cookies.Delete("isAutoLogin");
                httpContextAccessor.HttpContext.Response.Cookies.Delete(nameof(TokenOutput));
            }
            return Task.CompletedTask;
        }

        public Task Set(bool isAutoLogin, TokenOutput tokenOutput)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                string value = JsonSerializer.Serialize(tokenOutput);
                httpContextAccessor.HttpContext.Response.Cookies.Append("isAutoLogin", isAutoLogin.ToString());
                httpContextAccessor.HttpContext.Response.Cookies.Append(nameof(TokenOutput), value);
            }
            return Task.CompletedTask;
        }
    }
}
