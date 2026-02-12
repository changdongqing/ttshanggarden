// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.WoChat.Impl.Entities
{
    /// <summary>
    /// Im系统会话数据
    /// </summary>
    [Table("WoChat" + nameof(ImSession))]
    public class ImSession : ImSessionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 用户签名
        /// </summary>
        /// <remarks>
        /// 通过用户编号组，计算签名，方便从用户编号组进行查重
        /// userIds 正序，逗号拼接，MD5
        /// 目前仅用于私聊，群聊仅在创建时维护了，后续退出或加入时未重新维护
        /// </remarks>
        [Display(Name = nameof(WoChatResource.UsersSignature), ResourceType = typeof(WoChatResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string UsersSignature { get; set; } = null!;
    }
}
