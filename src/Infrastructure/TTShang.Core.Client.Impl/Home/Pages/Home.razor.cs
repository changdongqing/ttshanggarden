// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Settings;
using Microsoft.Extensions.Options;

namespace TTShang.Core.Client.Impl.Home.Pages
{
    public partial class Home : ReuseTabsPageBase
    {

        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; } = null!;
    }
}
