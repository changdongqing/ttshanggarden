using Gardener.Core.AppManager.Dtos;
using Gardener.Core.AppManager.Enums;

namespace Gardener.Core.AppManager.Services
{
    /// <summary>
    ///  应用版本服务
    /// </summary>
    public interface IAppVersionService : IServiceBase<AppVersionDto, Int64>
    {

        /// <summary>
        /// 查找新版本
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="version"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        Task<AppVersionDto?> FindNewVersion(string packageName, long version, AppEnvironments environment);

        /// <summary>
        /// 查询-获取最后一个版本
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        Task<AppVersionDto?> FindLastVersion(string packageName, AppEnvironments environment);

    }
}
