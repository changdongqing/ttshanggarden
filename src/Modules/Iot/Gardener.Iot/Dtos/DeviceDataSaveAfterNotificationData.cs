// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.NotificationSystem;

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备数据存储后通知
    /// </summary>
    public class DeviceDataSaveAfterNotificationData : DynamicNotificationData
    {
        /// <summary>
        /// 设备数据
        /// </summary>
        public DeviceDataDto DeviceData { get; set; }
        /// <summary>
        /// 设备数据存储后通知
        /// </summary>
        /// <param name="deviceData"></param>
        public DeviceDataSaveAfterNotificationData(DeviceDataDto deviceData) : base()
        {
            this.IsConsumOnce = false;
            this.DeviceData = deviceData;
            List<string> keys = new List<string>();
            if (DeviceData.DeviceId.HasValue)
            {
                keys.Add(nameof(DeviceDataDto.DeviceId) + DeviceData.DeviceId);
            }
            if (DeviceData.DeviceConnectionId.HasValue)
            {
                keys.Add(nameof(DeviceDataDto.DeviceConnectionId) + DeviceData.DeviceConnectionId);
            }
            keys.Add(nameof(DeviceDataDto.DeviceClientId) + DeviceData.DeviceClientId);
            this.EventKeys = keys.ToArray();
        }
    }
}
