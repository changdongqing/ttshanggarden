// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit.Dtos;
using Gardener.Core.Audit.Resources;

namespace Gardener.Core.Client.Impl.Audit.Pages
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
