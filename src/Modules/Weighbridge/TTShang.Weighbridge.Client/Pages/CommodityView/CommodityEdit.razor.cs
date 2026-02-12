// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Weighbridge.Client.Pages.CommodityView
{
    /// <summary>
    /// 编辑页
    /// </summary>
    public partial class CommodityEdit : EditOperationDialogBase<CommodityDto, Int32, WeighbridgeLocalResource>
    {
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.CommodityName, x => x.TenantId);
            _uniqueVerificationTool.AddField(x => x.CommodityCode, x => x.TenantId);
            base.OnInitialized();
        }
    }
}
