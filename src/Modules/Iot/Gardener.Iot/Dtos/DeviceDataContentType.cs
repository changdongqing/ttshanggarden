// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备数据类型
    /// </summary>
    public struct DeviceDataContentType
    {
        /// <summary>
        /// 设备数据类型
        /// </summary>
        /// <param name="contentType"></param>
        public DeviceDataContentType(string contentType)
        {
            ContentType = contentType;
        }
        /// <summary>
        /// 设备数据类型
        /// </summary>
        public string ContentType { get; }
        /// <summary>
        /// 设备数据类型
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ContentType;
        }
        /// <summary>
        /// Text
        /// </summary>
        public static readonly DeviceDataContentType Text = new DeviceDataContentType("text/plain");
        /// <summary>
        /// Json
        /// </summary>
        public static readonly DeviceDataContentType ApplicationJson = new DeviceDataContentType("application/json");
        /// <summary>
        /// Xml
        /// </summary>
        public static readonly DeviceDataContentType ApplicationXml = new DeviceDataContentType("application/xml");
        /// <summary>
        /// 未知
        /// </summary>
        public static readonly DeviceDataContentType Unknown = new DeviceDataContentType("unknown");
        /// <summary>
        /// 类型集合
        /// </summary>
        public static readonly IEnumerable<DeviceDataContentType> Types = [Text, ApplicationJson, ApplicationXml, Unknown];

    }
}
