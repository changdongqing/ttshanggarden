// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Audit.Entities;
using TTShang.Core.Audit.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TTShang.Core.Api.Impl.Audit.Internal
{
    /// <summary>
    /// 审计服务
    /// </summary>
    internal class AuditService<TDbContextLocator> : IAuditService where TDbContextLocator : class, IDbContextLocator
    {
        private readonly ILogger<AuditService<TDbContextLocator>> _logger;
        private readonly IRepository<AuditFunction, TDbContextLocator> _auditOperationRepository;
        private readonly IServiceScopeFactory _scopeFactory;
        private AuditFunction? _auditOperation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="auditOperationRepository"></param>
        /// <param name="scopeFactory"></param>
        public AuditService(ILogger<AuditService<TDbContextLocator>> logger,
            IRepository<AuditFunction, TDbContextLocator> auditOperationRepository,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _auditOperationRepository = auditOperationRepository;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditFunction(AuditFunction auditOperation)
        {
            if (auditOperation == null)
            {
                return Task.CompletedTask;
            }
            _auditOperation = auditOperation;
            try
            {
                return _auditOperationRepository.InsertNowAsync(auditOperation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "操作审计写入数据库异常");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 数据保存结束
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task DataSaveEnd(IEnumerable<AuditEntity>? entities)
        {
            if (_auditOperation == null || entities == null || !entities.Any())
            {
                return;
            }

            foreach (AuditEntity auditEntity in entities)
            {
                auditEntity.AuditFunctionId = _auditOperation.Id;
                auditEntity.OperaterId = _auditOperation.OperaterId;
                auditEntity.OperaterName = _auditOperation.OperaterName;
                auditEntity.OperaterType = _auditOperation.OperaterType;
                auditEntity.CreateBy = _auditOperation.CreateBy;
                auditEntity.CreateIdentityType = _auditOperation.CreateIdentityType;
                auditEntity.CreatedTime = DateTimeOffset.Now;
                auditEntity.TenantId = _auditOperation.TenantId;
                if (auditEntity.AuditProperties != null)
                {
                    foreach (AuditProperty  property in auditEntity.AuditProperties)
                    {
                        property.CreateBy = auditEntity.CreateBy;
                        property.CreateIdentityType = auditEntity.CreateIdentityType;
                        property.TenantId = auditEntity.TenantId;
                        property.CreatedTime = DateTimeOffset.Now;
                    }
                }
            }
            using (var scope = _scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                // 获取仓储
                var respository = Db.GetRepository<AuditEntity, TDbContextLocator>(services);
                await respository.InsertNowAsync(entities);
            }
        }

    }
}
