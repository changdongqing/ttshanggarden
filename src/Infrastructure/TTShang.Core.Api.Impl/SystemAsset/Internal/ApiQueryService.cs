// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.SystemAsset.Entities;
using TTShang.Core.Authorization.Services;
using TTShang.Core.Cache;

namespace TTShang.Core.Api.Impl.SystemAsset.Internal
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public class ApiQueryService : IApiQueryService
    {
        private readonly string cacheKeyPre = $"{nameof(Function)}:";
        private readonly ICache cache;
        private readonly IRepository<Function> functionRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="functionRepository"></param>
        public ApiQueryService(ICache cache, IRepository<Function> functionRepository)
        {
            this.cache = cache;
            this.functionRepository = functionRepository;
        }

        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetApiCacheKey(string key)
        {
            return cacheKeyPre + "fromKey:" + key;
        }
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetApiCacheKey(Guid id)
        {
            return cacheKeyPre + "from:id" + id;
        }

        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<FunctionDto?> Query(string key)
        {
            Func<Task<FunctionDto?>> func = async () =>
            {
                Function? function = await functionRepository.AsQueryable(false).Where(x => x.Key.Equals(key) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
                var result = function?.Adapt<FunctionDto>();
                //设置一个Id为key的缓存
                if (result != null)
                {
                    await cache.SetAsync(GetApiCacheKey(result.Id), result);
                }
                return result;
            };
            string cacheKey = GetApiCacheKey(key);
            return await cache.GetAsync(cacheKey, func);
        }

        /// <summary>
        /// 是否启用审计
        /// </summary>
        /// <remarks>
        /// 判断Api是否启用审计
        /// </remarks>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool?> IsEnableAudit(string key)
        {
            return (await Query(key))?.EnableAudit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal async Task ClearCache(Guid id)
        {
            var function = await cache.GetAsync<FunctionDto>(GetApiCacheKey(id));
            if (function != null)
            {
                await cache.RemoveAsync(GetApiCacheKey(id));
                await cache.RemoveAsync(GetApiCacheKey(function.Key));
            }
        }
    }
}
