// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Audit.Dtos;
using TTShang.Core.Audit.Resources;

namespace TTShang.Core.Client.Impl.Audit.Pages
{
    public partial class AuditEntity : ListTableBase<AuditEntityDto, Guid, AuditLocalResource>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(AuditEntityDto auditEntity)
        {
          await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(Localizer[nameof(AuditLocalResource.FieldChangeDetails)], new[] { auditEntity }, width: "960");
        }
    }
}
