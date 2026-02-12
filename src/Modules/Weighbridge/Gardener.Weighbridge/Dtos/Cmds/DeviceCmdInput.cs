// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 设备命令输入
    /// </summary>
    public class DeviceCmdInput
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid DeviceId { get; init; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelId { get; init; }
        
        /// <summary>
        /// 设备命令输入
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        public DeviceCmdInput(Guid deviceId,int channelId=1)
        {
            DeviceId = deviceId;
            ChannelId = channelId;
        }
    }

    /// <summary>
    /// 设备命令输入
    /// </summary>
    public class DeviceCmdInput<TConfig> : DeviceCmdInput
    {
        /// <summary>
        /// 配置数据
        /// </summary>
        public TConfig Config { get; set; }

        /// <summary>
        /// 设备命令输入
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="config"></param>
        public DeviceCmdInput(Guid deviceId, TConfig config) : base(deviceId)
        {
            Config = config;
        }
    }
}
