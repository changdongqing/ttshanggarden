// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Authorization
{
    /// <summary>
    /// 登录数据存取器
    /// </summary>
    public interface ILoginDataAccessor
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        Task<(bool, TokenOutput?)> Get();

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="isAutoLogin"></param>
        /// <param name="tokenOutput"></param>
        /// <returns></returns>
        Task Set(bool isAutoLogin, TokenOutput tokenOutput);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="isAutoLogin"></param>
        /// <returns></returns>
        Task Remove(bool isAutoLogin);

    }
}
