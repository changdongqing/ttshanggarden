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
    /// 字典列表
    /// </summary>
    public partial class Code : ListOperateTableBase<CodeDto, int, CodeEdit, CodeEditParams, OperationDialogOutput, DictResource,OperationDialogInput<int?>,OperationDialogOutput>
    {
        /// <summary>
        /// 字典类型服务
        /// </summary>
        [Inject]
        protected ICodeTypeService CodeTypeService { get; set; } = null!;

        private TableSize tableSize = ClientConstant.DefaultTableSize;
        protected override void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
        {
            //传入编号，以小table展示
            if (this.Options?.Data != null)
            {
                tableSize = TableSize.Small;
                _pageSize = 10;
                tableSearchSettings.DefaultValue.Add(nameof(CodeDto.CodeTypeId), this.Options.Data);
            }

            //搜索组件中 显示名称 CodeTypeId => CodeType
            tableSearchSettings.FieldDisplayNameConverts.Add(nameof(CodeDto.CodeTypeId),x=> nameof(CodeDto.CodeType));
            //CodeTypeId 设置下拉数据
            tableSearchSettings.FieldSelectItemsProviders.Add(nameof(CodeDto.CodeTypeId), async x =>
            {
               IEnumerable<CodeTypeDto> codeTypes=await CodeTypeService.GetAllUsable(includLocked: true);
                return codeTypes.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.CodeTypeName));
            });
            base.SetTableSearchParameters(tableSearchSettings, tableSearchFilterGroupProviders);
        }
        
        /// <summary>
        /// 点击添加后，处理输入弹框的参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override Task<bool> OnClickAddRunBefore(CodeEditParams input)
        {
            input.CodeTypeId = this.Options?.Data;

            return base.OnClickAddRunBefore(input);
        }
    }
}
