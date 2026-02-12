// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Entities;
using Gardener.Core.Audit.Dtos;
using Gardener.Core.Audit.Services;
using System.Linq.Expressions;

namespace Gardener.Core.Api.Impl.Audit.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class AuditEntityService : ServiceBase<AuditEntity, AuditEntityDto, Guid, GardenerMultiTenantDbContextLocator>, IAuditEntityService
    {
        private readonly IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> _auditRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditEntityService(IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
            _auditRepository = repository;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<AuditEntityDto>> Search(PageRequest request)
        {
            Expression<Func<AuditEntity, bool>> expression = FilterHelper.GetExpression<AuditEntity>(request.FilterGroups);

            IQueryable<AuditEntity> queryable = _auditRepository
                .AsQueryable(false)
                .Include(x => x.AuditProperties)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions)
                .Select(x => x.Adapt<AuditEntityDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
