// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.SystemAsset.Entities;
using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.UserCenter.Services;

namespace TTShang.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class TenantService : ServiceBase<SystemTenant, SystemTenantDto, Guid>, ITenantService
    {
        private readonly IRepository<SystemTenantResource, GardenerMultiTenantDbContextLocator> _tenantResourceRepository;
        /// <summary>
        /// 租户服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="tenantResourceRepository"></param>
        public TenantService(IRepository<SystemTenant> repository, IRepository<SystemTenantResource, GardenerMultiTenantDbContextLocator> tenantResourceRepository) : base(repository)
        {
            _tenantResourceRepository = tenantResourceRepository;
        }
        /// <summary>
        /// 为租户添加资源
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="resourceIds">资源编号</param>
        /// <returns></returns>
        public async Task<bool> AddResources([ApiSeat(ApiSeats.ActionStart)] Guid tenantId, [FromBody] Guid[] resourceIds)
        {
            List<SystemTenantResource> tenantResources = await _tenantResourceRepository.AsQueryable(false).Where(x => x.TenantId.Equals(tenantId)).ToListAsync();
            List<SystemTenantResource> needDeleteList = new List<SystemTenantResource>();
            List<Guid> needAddList = new List<Guid>();
            foreach (var resourceId in resourceIds)
            {
                if (!tenantResources.Any(x => x.ResourceId.Equals(resourceId)))
                {
                    needAddList.Add(resourceId);
                }
            }
            foreach (var item in tenantResources)
            {
                if (!resourceIds.Any(x => x.Equals(item.ResourceId)))
                {
                    needDeleteList.Add(item);
                }
            }
            //删除
            foreach (SystemTenantResource tenantResource in needDeleteList)
            {
                await _tenantResourceRepository.DeleteAsync(tenantResource);
            }
            if (needAddList.Any())
            {
                //写入
                await _tenantResourceRepository.InsertAsync(needAddList.Select(x => new SystemTenantResource() { TenantId = tenantId, ResourceId = x }));
            }
            return true;
        }
        /// <summary>
        /// 获取租户资源列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ResourceDto>> GetResources([ApiSeat(ApiSeats.ActionStart)] Guid tenantId)
        {
            List<Resource> resources = await _tenantResourceRepository
                    .Include(x => x.Resource)
                    .Where(x => x.TenantId.Equals(tenantId))
                    .Select(x => x.Resource)
                    .OrderBy(x => x.Order)
                    .ToListAsync();
            return resources.Select(x => x.Adapt<ResourceDto>());
        }

        /// <summary>
        /// 复制一个新租户
        /// </summary>
        /// <remarks>
        /// 从现有租户，复制出一个新租户，租户的资源权限将保持一致。
        /// </remarks>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CopyNewTenant([ApiSeat(ApiSeats.ActionStart)] Guid tenantId)
        {
            var oldTenant = await Find(tenantId);

            if (oldTenant == null)
            {
                return false;
            }

            SystemTenantDto tenant = new SystemTenantDto()
            {
                Name = "New Copy "+DateTime.Now.ToString("yyyyMMddHHmmss")
            };

            tenant = await base.Insert(tenant);

            List<Guid> resourceIds = await _tenantResourceRepository
                   .Where(x => x.TenantId.Equals(tenantId))
                   .Select(x => x.ResourceId)
                   .ToListAsync();

            if (resourceIds.Any())
            {
                List<SystemTenantResource> tenantResources = resourceIds.Select(x => new SystemTenantResource()
                {

                    ResourceId = x,
                    TenantId = tenant.Id

                }).ToList();
                await _tenantResourceRepository.InsertAsync(tenantResources);
            }
            return true;
        }
    }
}
