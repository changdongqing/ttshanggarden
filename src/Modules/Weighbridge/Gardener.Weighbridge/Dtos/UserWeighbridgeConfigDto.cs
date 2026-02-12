using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 用户地磅配置
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.UserWeighbridgeConfig), ResourceType = typeof(WeighbridgeLocalResource))]
    public class UserWeighbridgeConfigDto : TenantBaseDto<int>
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.UserId), ResourceType = typeof(WeighbridgeLocalResource))]
        public int UserId { get; set; }

        /// <summary>
        /// 默认地磅配置编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.DefaultWeighbridgeConfigId), ResourceType = typeof(WeighbridgeLocalResource))]
        public Guid? DefaultWeighbridgeConfigId {  get; set; }

        /// <summary>
        /// 默认打印模板键
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.DefaultPrintTemplateKey), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? DefaultPrintTemplateKey {  get; set; }
    }
}
