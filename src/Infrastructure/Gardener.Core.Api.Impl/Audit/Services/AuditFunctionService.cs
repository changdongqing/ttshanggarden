// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Entities;
using Gardener.Core.Audit.Dtos;
using Gardener.Core.Audit.Services;
using Gardener.Core.Swagger.Services;

namespace Gardener.Core.Api.Impl.Audit.Services
{
    /// <summary>
    /// 审计功能服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class AuditFunctionService : ServiceBase<AuditFunction, AuditFunctionDto, Guid, GardenerMultiTenantDbContextLocator>, IAuditFunctionService
    {
        private readonly IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> _auditEntityRepository;
        private readonly IApiEndpointService _apiEndpointService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="auditEntityRepository"></param>
        /// <param name="apiEndpointService"></param>
        public AuditFunctionService(IRepository<AuditFunction, GardenerMultiTenantDbContextLocator> repository, IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> auditEntityRepository, IApiEndpointService apiEndpointService) : base(repository)
        {
            _auditEntityRepository = auditEntityRepository;
            _apiEndpointService = apiEndpointService;
        }

        /// <summary>
        /// 根据操作审计ID获取数据审计
        /// </summary>
        /// <remarks>
        /// 根据操作审计ID获取数据审计
        /// </remarks>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<List<AuditEntityDto>> GetAuditEntity([ApiSeat(ApiSeats.ActionStart)] Guid operationId)
        {
            return await _auditEntityRepository
                .Include(x => x.AuditProperties)
                .Where(x => x.IsDeleted == false && x.AuditFunctionId == operationId).Select(x => x.Adapt<AuditEntityDto>()).ToListAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<AuditFunctionDto>> Search(PageRequest request)
        {
            PageList<AuditFunctionDto> result = await base.Search(request);
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    if (item.FunctionKey == null) continue;
                    item.ApiEndpoint = await _apiEndpointService.GetApi(item.FunctionKey);
                }
            }

            return result;
        }
    }
}
