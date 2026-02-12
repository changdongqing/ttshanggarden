// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit.Dtos;

namespace Gardener.Core.Audit.Services
{
    /// <summary>
    /// 审计功能服务
    /// </summary>
    public interface IAuditFunctionService : IServiceBase<AuditFunctionDto, Guid>
    {
        /// <summary>
        /// 根据操作审计ID获取数据审计数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId);
    }
}
