// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Settings
{
    /// <summary>
    /// api的配置
    /// 如何装配需要看<see cref="Client.Core.ApiSettingExtension"/>
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string BaseAddres { get { return Host + ":" + Port + "/" + BasePath + "/"; } }
        /// <summary>
        /// 
        /// </summary>
        public string? Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? BasePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? UploadPath { get; set; }

    }
}
