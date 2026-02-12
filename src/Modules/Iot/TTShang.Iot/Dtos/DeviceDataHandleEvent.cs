// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataHandleEvent : EventBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="receivedTime"></param>
        public DeviceDataHandleEvent(string clientId, DeviceConnectionType deviceConnectionType, DateTimeOffset receivedTime) : base()
        {
            this.DeviceClientId = clientId;
            this.DeviceConnectionType = deviceConnectionType;
            this.ReceivedTime = receivedTime;
        }
        /// <summary>
        /// 
        /// </summary>
        public DeviceConnectionType DeviceConnectionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceClientId { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public DeviceDataContentType? ContentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[]? Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>>? UserProperties { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DeviceConnectionDto? DeviceConnection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DeviceDto? Device { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object>? ExtendData { get; set; }
        /// <summary>
        /// 接收到的时间
        /// </summary>
        public DateTimeOffset ReceivedTime {  get; set; }
    }
}
