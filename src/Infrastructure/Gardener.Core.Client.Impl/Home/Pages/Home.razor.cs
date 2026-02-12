// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Settings;
using Microsoft.Extensions.Options;

namespace Gardener.Core.Client.Impl.Home.Pages
{
    public partial class Home : ReuseTabsPageBase
    {

        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; } = null!;
    }
}
