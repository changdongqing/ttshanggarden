// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.OperationDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Iot.Client.Pages.DeviceView
{
    public partial class GenerateConnectConfig: OperationDialogBase<DeviceDto,bool,IotLocalResource>
    {
        string tcpConnectInfo=string.Empty;
        string tcpPingContent=string.Empty;
        private void OnTabChange(string key)
        {
            if ("Tcp".Equals(key))
            {

                tcpPingContent = "Heartbeat";
                tcpConnectInfo = $"Login;{this.Options.ClientId};{this.Options.Account};{this.Options.SecretKey}";
            }
        }
    }
}
