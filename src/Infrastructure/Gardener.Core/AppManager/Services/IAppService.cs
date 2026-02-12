// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.AppManager.Dtos;

namespace Gardener.Core.AppManager.Services
{
    /// <summary>
    ///  应用服务
    /// </summary>
    public interface IAppService : IServiceBase<AppDto, Guid>
    {
        /// <summary>
        /// 查询-根据包名查询app信息
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        Task<AppDto?> FindByPackageName(string packageName);
    }
}
