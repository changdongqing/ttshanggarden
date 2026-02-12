// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.SystemAsset.Entities;
using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.Authorization.Services;
using Gardener.Core.Dtos.Constraints;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Api.Impl.SystemAsset.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class ResourceService : ServiceBase<Resource, ResourceDto, Guid>, IResourceService
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<SystemTenantResource> _tenantResourceRepository;
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 资源服务
        /// </summary>
        /// <param name="resourceRepository"></param>
        /// <param name="resourceFunctionRespository"></param>
        /// <param name="tenantResourceRepository"></param>
        /// <param name="identityService"></param>
        public ResourceService(IRepository<Resource> resourceRepository, IRepository<ResourceFunction> resourceFunctionRespository, IRepository<SystemTenantResource> tenantResourceRepository, IIdentityService identityService) : base(resourceRepository)
        {
            _resourceRepository = resourceRepository;
            _resourceFunctionRespository = resourceFunctionRespository;
            _tenantResourceRepository = tenantResourceRepository;
            _identityService = identityService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据父级编号查询所有子级资源
        /// </remarks>
        /// <param name="id">父id</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            var resources = await _resourceRepository
                .Where(x => x.ParentId == id)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Order)
               .Select(x => x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }


        /// <summary>
        /// 查询根节点
        /// </summary>
        /// <remarks>
        /// 查询所有根节点
        /// </remarks>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetRoot()
        {
            var resources = await _resourceRepository
                .Where(x => x.ParentId == null && x.Type.Equals(ResourceType.Root))
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 查询所有资源,按树形结构返回
        /// <para>
        /// 非租户在所有资源中抽取，租户在自己的资源池中抽取
        /// </para>
        /// </remarks>
        /// <param name="includLocked">是否包含锁定的资源</param>
        /// <param name="rootKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="supportMultiTenant">是否支持多租户</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetTree([FromQuery] bool includLocked = true, [FromQuery] string? rootKey = null, [FromQuery] Guid? tenantId = null, [FromQuery] bool? supportMultiTenant = null)
        {

            List<Resource>? resources = null;
            IModelTenant? identity = _identityService.GetIdentity() as IModelTenant;
            if (tenantId != null || identity != null && identity.IsTenant)
            {
                //查询租户
                //租户只能查自己租户的
                tenantId = identity != null && identity.IsTenant ? identity.TenantId : tenantId;
                resources = await _tenantResourceRepository
                    .Include(x => x.Resource)
                    .Where(tenantId != null, x => x.TenantId.Equals(tenantId))
                    .Select(x => x.Resource)
                    .Where(!string.IsNullOrEmpty(rootKey), x => x.Key.Equals(rootKey))
                    .Where(supportMultiTenant != null, x => x.SupportMultiTenant.Equals(supportMultiTenant))
                    .OrderBy(x => x.Order)
                    .ToListAsync();
            }
            else
            {
                //直接查询
                resources = await _resourceRepository
                   .Where(x => x.IsDeleted == false && (includLocked || x.IsLocked == false))
                   .Where(!string.IsNullOrEmpty(rootKey), x => x.Key.Equals(rootKey))
                   .Where(supportMultiTenant != null, x => x.SupportMultiTenant.Equals(supportMultiTenant))
                   .OrderBy(x => x.Order)
                   .ToListAsync();
            }
            return resources.Where(x => x.Type.Equals(ResourceType.Root)).Select(x => x.Adapt<ResourceDto>()).ToList();
        }

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <remarks>
        /// 添加资源
        /// </remarks>
        /// <param name="resourceDto"></param>
        /// <returns></returns>
        public override async Task<ResourceDto> Insert(ResourceDto resourceDto)
        {
            if (_resourceRepository.Any(x => x.Key.Equals(resourceDto.Key) && x.IsDeleted == false && x.IsLocked == false, false))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Resource_Key_Repeat);
            }
            return await base.Insert(resourceDto);
        }
        /// <summary>
        /// 查询接口
        /// </summary>
        /// <remarks>
        /// 根据资源id获取已绑定接口
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<FunctionDto>> GetFunctions([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            List<FunctionDto> functions = await _resourceFunctionRespository.AsQueryable(false)
                 .Include(x => x.Function)
                 .Where(x => x.ResourceId.Equals(id))
                 .Select(x => x.Function)
                 .Where(x => x != null && x.IsDeleted == false && x.IsLocked == false)
                 .Select(x => x.Adapt<FunctionDto>())
                 .ToListAsync();
            return functions;
        }

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public new async Task<string> GenerateSeedData(PageRequest request)
        {
            IQueryable<Resource> queryable = GetSearchQueryable(request.FilterGroups);
            PageList<Resource> result = await queryable
                .Include(x => x.ResourceFunctions)
                .OrderConditions(request.OrderConditions.ToArray())
                .ToPageAsync(request.PageIndex, request.PageSize);
            List<string> excludeFields = new List<string>();
            excludeFields.AddRange([nameof(IModelUpdated.UpdateBy), nameof(IModelUpdated.UpdateIdentityType), nameof(IModelUpdated.UpdatedTime)]);
            excludeFields.AddRange([nameof(IModelCreated.CreateBy), nameof(IModelCreated.CreateIdentityType), nameof(IModelCreated.CreatedTime)]);
            return SeedDataGenerateHelper.Generate(result.Items.Select(x => x.Adapt<ResourceDto>()), typeof(ResourceDto).Name, excludeFields: excludeFields.ToArray());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(Guid id)
        {
            await base.TreeDataDelete(id, x => x.ParentId, x => x.Id);
            return true;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override async Task<bool> Deletes([FromBody] Guid[] ids)
        {
            foreach (var id in ids)
            {
                await Delete(id);
            }
            return true;
        }

    }
}
