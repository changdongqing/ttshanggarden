// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Gardener.Iot.Client.Pages.DeviceGroupView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DeviceGroupEdit : EditOperationDialogBase<DeviceGroupDto, int, IotLocalResource>
    {
        [Inject]
        IDeviceGroupService deviceGroupService { get; set; } = null!;

        //树
        List<DeviceGroupDto> allDatas = new List<DeviceGroupDto>();
        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string _parentDeptId
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
            allDatas = await deviceGroupService.GetTree(true);
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override void OnDataLoaded()
        {
            //数据加载后
            OperationDialogInput<int> editInput = this.Options;
            if (editInput.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.ParentId = editInput.Data == 0 ? null : editInput.Data;
            }
        }
    }
}
