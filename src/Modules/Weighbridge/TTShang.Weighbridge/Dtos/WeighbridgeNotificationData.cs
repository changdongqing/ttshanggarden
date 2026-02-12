// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.NotificationSystem;

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 地磅数据通知
    /// </summary>
    public class WeighbridgeNotificationData : DynamicNotificationData
    {
        /// <summary>
        /// 地磅数据通知
        /// </summary>
        public WeighbridgeUploadData? Data { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Guid DeviceId { get; set; }
        /// <summary>
        /// 设备数据存储后通知
        /// </summary>
        /// <param name="deviceId"></param>
        public WeighbridgeNotificationData(Guid deviceId) : base()
        {
            DeviceId = deviceId;
            this.EventKeys = [GetEventKey(deviceId)];
        }
        /// <summary>
        /// 获取事件key
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static string GetEventKey(Guid deviceId)
        {
            return nameof(WeighbridgeNotificationData.DeviceId) + deviceId;
        }
    }
}
