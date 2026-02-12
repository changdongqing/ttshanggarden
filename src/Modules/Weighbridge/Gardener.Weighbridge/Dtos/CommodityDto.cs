// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attributes;
using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.Commodity), ResourceType = typeof(WeighbridgeLocalResource))]
    public class CommodityDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityName), ResourceType = typeof(WeighbridgeLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CommodityName { get; set; } = default!;

        /// <summary>
        /// 商品编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityCode), ResourceType = typeof(WeighbridgeLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CommodityCode { get; set; } = default!;

        /// <summary>
        /// 商品描述
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityDescription), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? CommodityDescription { get; set; } = default!;

    }
}
