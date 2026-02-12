// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.JsTool;
using System.Text.Json;

namespace Gardener.Core.Client.Impl.Authorization
{
    /// <summary>
    /// wasm 登录数据存取器
    /// </summary>
    public class WasmLoginDataAccessor : ILoginDataAccessor
    {
        private readonly IJsTool jsTool;

        public WasmLoginDataAccessor(IJsTool jsTool)
        {
            this.jsTool = jsTool;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IWebStorage GetWebStorageFromAutoLogin(bool isAutoLogin)
        {
            return isAutoLogin ? jsTool.LocalStorage : jsTool.SessionStorage;
        }
        public async Task<(bool, TokenOutput?)> Get()
        {
            //自动登录
            var isAutoLoginStr = await jsTool.LocalStorage.GetAsync<string>("isAutoLogin");
            bool isAutoLogin = string.IsNullOrEmpty(isAutoLoginStr) ? false : bool.Parse(isAutoLoginStr);
            //token
            var value = await GetWebStorageFromAutoLogin(isAutoLogin).GetAsync<string>(nameof(TokenOutput));
            if (value == null)
            {
                return (isAutoLogin, null);
            }
            TokenOutput? tokenOutput = JsonSerializer.Deserialize<TokenOutput>(value);
            return (isAutoLogin, tokenOutput);
        }

        public Task Remove(bool isAutoLogin)
        {
            //浏览器本地 clear
            var task1 = GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput));
            var task2 = jsTool.LocalStorage.RemoveAsync("isAutoLogin");
            var task3 = jsTool.Cookie.Remove("isAutoLogin");
            var task4 = jsTool.Cookie.Remove(nameof(TokenOutput));
            return Task.WhenAll(task1, task2, task3, task4);
        }

        public Task Set(bool isAutoLogin, TokenOutput tokenOutput)
        {
            string value = JsonSerializer.Serialize(tokenOutput);
            var task1 = GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput), value);
            var task2 = jsTool.LocalStorage.SetAsync("isAutoLogin", isAutoLogin);

            var task3 = jsTool.Cookie.Set("isAutoLogin", isAutoLogin.ToString());
            var task4 = jsTool.Cookie.Set(nameof(TokenOutput), value);

            return Task.WhenAll(task1, task2, task3, task4);
        }
    }
}
