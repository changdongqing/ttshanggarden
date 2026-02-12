// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.AppManager.Pages.AppView
{
    /// <summary>
    /// 应用编辑页
    /// </summary>
    public partial class AppEdit : EditOperationDialogBase<AppDto, Guid, AppManagerResource>
    {
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {

            _uniqueVerificationTool.AddField(x => x.AppName);
            _uniqueVerificationTool.AddField(x => x.PackageName);
            base.OnInitialized();
        }
    }
}
