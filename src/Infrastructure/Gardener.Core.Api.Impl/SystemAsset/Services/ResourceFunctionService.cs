// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.SystemAsset.Entities;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Api.Impl.SystemAsset.Services
{

    /// <summary>
    /// 资源与接口关系服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class ResourceFunctionService : ServiceBaseNoKey<ResourceFunction, ResourceFunctionDto>, IResourceFunctionService
    {
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFunctionRespository"></param>
        public ResourceFunctionService(IRepository<ResourceFunction> resourceFunctionRespository) : base(resourceFunctionRespository)
        {
            _resourceFunctionRespository = resourceFunctionRespository;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 删除资源与接口关系
        /// </remarks>
        /// <param name="resourceId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete([FromRoute] Guid resourceId, [FromRoute] Guid functionId)
        {
            List<ResourceFunction> entitys = await _resourceFunctionRespository.Where(x => x.ResourceId.Equals(resourceId) && x.FunctionId.Equals(functionId)).ToListAsync();

            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }

            await _resourceFunctionRespository.DeleteAsync(entitys);

            return true;
        }

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <remarks>
        /// 根据资源编号获取种子数据
        /// </remarks>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetSeedData([FromBody] List<Guid> resourceIds)
        {
            if (resourceIds == null)
            {
                resourceIds = new List<Guid>(0);
            }
            List<ResourceFunction> list = await _resourceFunctionRespository.Where(x => resourceIds.Any(r => r.Equals(x.ResourceId))).OrderBy(x => x.ResourceId).ToListAsync();
            return SeedDataGenerateHelper.Generate(list, typeof(ResourceFunction).Name);
        }
    }
}
