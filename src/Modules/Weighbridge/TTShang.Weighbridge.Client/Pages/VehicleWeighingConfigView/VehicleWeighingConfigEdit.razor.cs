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

namespace TTShang.Weighbridge.Client.Pages.VehicleWeighingConfigView
{
    /// <summary>
    /// 编辑页
    /// </summary>
    public partial class VehicleWeighingConfigEdit : EditOperationDialogBase<VehicleWeighingConfigDto, Int32, WeighbridgeLocalResource>
    {
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {

            _uniqueVerificationTool.AddField(x => x.PlateNumber,x=>x.TenantId);
            base.OnInitialized();
        }
    }
}
