// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Resources;

namespace Gardener.Core.Client.Impl.Authorization.Pages.LoginLogView
{
    /// <summary>
    /// 登录日志列表页
    /// </summary>
    public partial class LoginLog : ListOperateTableBase<LoginLogDto, long, LoginLogEdit, AuthorizationLocalResource>
    {
    }
}
