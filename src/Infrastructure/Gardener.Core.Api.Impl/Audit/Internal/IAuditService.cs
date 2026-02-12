// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Entities;
using Gardener.Core.Audit.Dtos;

namespace Gardener.Core.Api.Impl.Audit.Internal
{
    /// <summary>
    /// 审计服务接口
    /// </summary>
    internal interface IAuditService
    {
        /// <summary>
        /// 保存功能审计数据
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditFunction(AuditFunction auditOperation);

        /// <summary>
        /// 数据保存结束
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task DataSaveEnd(IEnumerable<AuditEntity>? entities);

    }
}
