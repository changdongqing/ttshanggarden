// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.UserCenter.Dtos
{
    /// <summary>
    /// 客户端功能信息
    /// </summary>
    [Display(Name = nameof(UserCenterResource.ClientFunction), ResourceType = typeof(UserCenterResource))]
    public class ClientFunctionDto : BaseDto
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [Display(Name = nameof(UserCenterResource.ClientId), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.FunctionId), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid FunctionId { get; set; }

    }
}
