// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.AppManager.Enums;
using System;

namespace TTShang.Core.Client.Impl.AppManager.Services
{
    /// <summary>
    ///  应用版本服务
    /// </summary>
    [ScopedService]
    public class AppVersionService : ClientServiceBase<AppVersionDto, Int64>, IAppVersionService
    {
        /// <summary>
        ///  应用版本服务
        /// </summary>
        public AppVersionService(IApiCaller apiCaller) : base(apiCaller, "app-version", "app-manager")
        {
        }

        public Task<AppVersionDto?> FindLastVersion(string packageName, AppEnvironments environment)
        {
            return apiCaller.GetAsync<AppVersionDto?>($"{baseUrl}/last-version/{packageName}/{environment}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="version"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public Task<AppVersionDto?> FindNewVersion(string packageName, long version, AppEnvironments environment)
        {
            return apiCaller.GetAsync<AppVersionDto?>($"{baseUrl}/new-version/{packageName}/{version}/{environment}");
        }
    }

}
