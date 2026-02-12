// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Dtos;
using Gardener.Core.Dict.Resources;
using Gardener.Core.Dict.Services;

namespace Gardener.Core.Client.Impl.Dict.Pages.CodeView
{
    /// <summary>
    /// 字典类型列表
    /// </summary>
    public partial class CodeType : ListOperateTableBase<CodeTypeDto, int, CodeTypeEdit, DictResource>
    {
        [Inject]
        private ICodeTypeService codeTypeService { get; set; } = null!;
        /// <summary>
        /// 显示字典列表
        /// </summary>
        /// <param name="codeType"></param>
        private async Task OnClickShowCodesManager(CodeTypeDto codeType)
        {
            await OpenOperationDialogAsync<Code, OperationDialogInput<int?>, OperationDialogOutput>(codeType.CodeTypeName, OperationDialogInput<int?>.Other(codeType.Id), width: "1200", onClose: ot =>
            {

                System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(ot));
                return Task.CompletedTask;
            });
        }
        /// <summary>
        /// 刷新字典工具缓存
        /// </summary>
        /// <returns></returns>
        private async Task OnClickRefreshDictHelperCache()
        {
            bool result = await codeTypeService.RefreshDictHelperCache();
            if (result)
            {
                MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Refresh), nameof(SharedLocalResource.Success)));
            }
            else
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Refresh), nameof(SharedLocalResource.Fail)));
            }
        }
    }
}
