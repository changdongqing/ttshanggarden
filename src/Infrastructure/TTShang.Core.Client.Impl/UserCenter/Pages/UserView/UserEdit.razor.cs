// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Pages.UserView
{
    public partial class UserEdit : EditOperationDialogBase<UserDto, int, UserCenterResource>
    {
        private List<DeptDto>? deptDatas;
        private List<PositionDto>? positions;
        [Inject]
        private IDeptService DeptService { get; set; } = null!;
        [Inject]
        private IPositionService PositionService { get; set; } = null!;

        //部门树
        /// <summary>
        /// 部门编号
        /// </summary>
        protected string? DeptId
        {
            get
            {
                return _editModel.DeptId?.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.DeptId = int.Parse(value);
                }
                else
                {
                    _editModel.DeptId = null;
                }
            }
        }
        protected override async Task OnDataLoadingAsync()
        {
            //岗位
            positions =await PositionService.GetAllUsable();
            //部门
            deptDatas =await DeptService.GetTree();
            await base.OnDataLoadingAsync();
        }
        protected override void OnDataLoaded()
        {
            if (_editModel != null)
            {
                _editModel.Password = null;
            }
            base.OnDataLoaded();
        }
       
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            string avatarDrawerWidth = "300";
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(
                Localizer[nameof(SharedLocalResource.UplaodAvatar)],
                new UserUploadAvatarParams(user, false),
                width: avatarDrawerWidth);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        static private void OnSelectedItemChangedHandler(PositionDto value)
        {

        }
    }
}
