// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit.Dtos;
using Gardener.Core.Audit.Services;

namespace Gardener.Core.Client.Impl.Audit.Services
{
    [ScopedService]
    public class AuditFunctionService : ClientServiceBase<AuditFunctionDto, Guid>, IAuditFunctionService
    {
        public AuditFunctionService(IApiCaller apiCaller) : base(apiCaller, "audit-function")
        {
        }

        public async Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId)
        {
            return await apiCaller.GetAsync<List<AuditEntityDto>>($"{this.baseUrl}/{operationId}/audit-entity");
        }
    }
}
