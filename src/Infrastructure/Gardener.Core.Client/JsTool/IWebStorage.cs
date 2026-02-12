// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.JsTool
{
    public interface IWebStorage
    {
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task SetAsync(string key, object value);
    }
}
