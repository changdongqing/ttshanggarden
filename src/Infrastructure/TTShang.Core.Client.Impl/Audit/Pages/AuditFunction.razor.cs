// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Audit.Dtos;
using TTShang.Core.Audit.Resources;
using TTShang.Core.Audit.Services;

namespace TTShang.Core.Client.Impl.Audit.Pages
{
    public partial class AuditFunction : ListTableBase<AuditFunctionDto, Guid, AuditLocalResource>
    {
        [Inject]
        public IAuditFunctionService auditOperationService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto> auditEntityDtos = await auditOperationService.GetAuditEntity(id);

            await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(Localizer[nameof(SharedLocalResource.Detail)], auditEntityDtos, width: "960");
        }

        /// <summary>
        /// 查看参数
        /// </summary>
        /// <returns></returns>
        private Task OnShowParametersClick(AuditFunctionDto dto)
        {
            return OpenOperationDialogAsync<ShowCode, ShowCodeOptions, bool>(
                        Localizer[nameof(AuditLocalResource.Parameters)],
                       new ShowCodeOptions()
                       {
                           Code = Task.FromResult(dto.Parameters ?? string.Empty),
                           Language = "json"
                       },
                        width: "1300"); ;
        }
    }
}
