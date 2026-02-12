// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


namespace TTShang.Core.Client.Impl.AppManager.Services
{
    /// <summary>
    ///  应用服务
    /// </summary>
    [ScopedService]
    public class AppService : ClientServiceBase<AppDto, Guid>, IAppService
    {
        /// <summary>
        ///  应用服务
        /// </summary>
        public AppService(IApiCaller apiCaller) : base(apiCaller, "app", "app-manager")
        {
        }

        public Task<AppDto?> FindByPackageName(string packageName)
        {
            return apiCaller.GetAsync<AppDto?>($"{baseUrl}/by-packageName/{packageName}");
        }
    }
}
