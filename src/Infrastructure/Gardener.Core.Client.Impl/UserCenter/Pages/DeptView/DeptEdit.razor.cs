// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Pages.DeptView
{
    public partial class DeptEdit : EditOperationDialogBase<DeptDto, int, UserCenterResource>
    {
        [Inject]
        IDeptService deptService { get; set; } = null!;

        //部门树
        List<DeptDto> deptDatas = new List<DeptDto>();
        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string ParentDeptId
        {
            get { return _editModel.ParentId?.ToString() ?? string.Empty; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.ParentId = int.Parse(value);
                }
                else
                {
                    _editModel.ParentId = null;
                }

            }
        }
        protected override async Task OnDataLoadingAsync()
        {
            deptDatas = await deptService.GetTree(true);
            await base.OnDataLoadingAsync();
        }

        protected override void OnDataLoaded()
        {
            //父级
            OperationDialogInput<int> editInput = this.Options;
            if (editInput.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.ParentId = editInput.Data == 0 ? null : editInput.Data;
            }
            base.OnDataLoaded();
        }
    }
}
