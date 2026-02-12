// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Api.Impl.LicensePlateRecognition.Internal
{
    /// <summary>
    /// open anpr配置
    /// </summary>
    public class OpenAnprSettings : IConfigurableOptions
    {
        /// <summary>
        /// api接口
        /// </summary>
        [Required]
        public required string ApiUrl {  get; set; }
        
    }
}
