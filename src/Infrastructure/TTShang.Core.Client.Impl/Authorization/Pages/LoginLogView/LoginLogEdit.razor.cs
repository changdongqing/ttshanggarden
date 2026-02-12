// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization.Resources;

namespace TTShang.Core.Client.Impl.Authorization.Pages.LoginLogView
{
    /// <summary>
    /// 登录日志编辑页
    /// </summary>
    public partial class LoginLogEdit : EditOperationDialogBase<LoginLogDto, long, AuthorizationLocalResource>
    {
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
