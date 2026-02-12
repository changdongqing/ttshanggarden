// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.SystemAsset.Entities;
using Gardener.Core.Swagger.Services;
using Gardener.Core.SystemAsset.Services;
using System.Web;


namespace Gardener.Core.Api.Impl.SystemAsset.Services
{
    /// <summary>
    /// 功能服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class FunctionService : ServiceBase<Function, FunctionDto, Guid>, IFunctionService
    {

        private readonly IApiEndpointService apiEndpointService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="apiEndpointService"></param>
        public FunctionService(IRepository<Function> repository, IApiEndpointService apiEndpointService) : base(repository)
        {
            this.apiEndpointService = apiEndpointService;
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <remarks>
        /// 启用或禁用功能
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="enableAudit"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EnableAudit([ApiSeat(ApiSeats.ActionStart)] Guid id, bool enableAudit = true)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null) return false;
            entity.EnableAudit = enableAudit;
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            await _repository.UpdateIncludeAsync(entity, new[] { nameof(Function.EnableAudit), nameof(Function.UpdatedTime) });
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// 根据 ApiHttpMethod 和 path 判断是否存在
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Exists(ApiHttpMethod method, string path)
        {
            path = HttpUtility.UrlDecode(path);

            return await _repository.Where(x => x.Method.Equals(method) && path.Equals(x.Path), tracking: false).AnyAsync();
        }

        /// <summary>
        /// 根据key获取
        /// </summary>
        /// <remarks>
        /// 根据key获取 功能点信息（是程序中最新信息）
        /// </remarks>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<FunctionDto?> GetByKey(string key)
        {
            Function? function = await _repository.Where(x => x.Key.Equals(key), tracking: false).FirstOrDefaultAsync();
            FunctionDto? result = function?.Adapt<FunctionDto>();
            if (result != null)
            {
                await FillCurrentApiInfo(result);
            }
            return result;
        }


        /// <summary>
        /// 填充最新api信息
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private async Task<FunctionDto> FillCurrentApiInfo(FunctionDto function)
        {
            var api = await apiEndpointService.GetApi(function.Key);
            if (api != null)
            {
                function.FillApiInfo(api);
            }
            return function;
        }
    }
}
