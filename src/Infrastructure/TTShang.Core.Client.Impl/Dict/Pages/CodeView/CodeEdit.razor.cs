// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;
using TTShang.Core.Dict.Resources;
using TTShang.Core.Dict.Services;

namespace TTShang.Core.Client.Impl.Dict.Pages.CodeView
{
    /// <summary>
    /// 字典编辑框
    /// </summary>
    public partial class CodeEdit : EditOperationDialogBase<CodeDto,int, DictResource, CodeEditParams, OperationDialogOutput>
    {
        [Inject]
        protected ICodeTypeService CodeTypeService { get; set; } = null!;
        /// <summary>
        /// 字典类型
        /// </summary>
        private List<CodeTypeDto>? codeTypeDtos;
        protected override void OnDataLoaded()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Add) && this.Options.CodeTypeId != null)
            {
                this._editModel.CodeTypeId = this.Options.CodeTypeId.Value;
            }
            base.OnDataLoaded();
        }
        protected override async Task OnDataLoadingAsync()
        {
            codeTypeDtos = await CodeTypeService.GetAllUsable();
            await base.OnDataLoadingAsync();
        }
    }
}
