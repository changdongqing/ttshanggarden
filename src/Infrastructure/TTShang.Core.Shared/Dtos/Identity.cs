// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TTShang.Core.Dtos
{
    /// <summary>
    /// 身份信息
    /// </summary>
    [Description("身份信息")]
    public class Identity : IModelTenantId
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="identityType"></param>
        public Identity(string id, IdentityType identityType)
        {
            Id = id;
            IdentityType = identityType;
            LoginClientType = LoginClientType.Unknown;
            LoginId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="identityType"></param>
        /// <param name="loginClientType"></param>
        /// <param name="loginId"></param>
        
        public Identity(string id, IdentityType identityType, LoginClientType loginClientType, string loginId)
        {
            Id = id;
            IdentityType = identityType;
            LoginClientType = loginClientType;
            LoginId = loginId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="nickName"></param>
        /// <param name="identityType"></param>
        /// <param name="loginClientType"></param>
        /// <param name="loginId"></param>
        /// <param name="tenantId"></param>
        /// <param name="customData"></param>
        /// <param name="clientName"></param>
        /// <param name="clientVersion"></param>
        [JsonConstructor]
        public Identity(string id, string? name, string? nickName, IdentityType identityType, LoginClientType loginClientType, string loginId, Guid? tenantId, string? customData, string? clientName, string? clientVersion)
        {
            Id = id;
            Name = name;
            NickName = nickName;
            IdentityType = identityType;
            LoginClientType = loginClientType;
            LoginId = loginId;
            TenantId = tenantId;
            CustomData = customData;
            ClientName = clientName;
            ClientVersion = clientVersion;
        }

        /// <summary>
        /// 身份唯一编号
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 身份唯一名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 身份昵称
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }
        /// <summary>
        /// 获取或设置 登录Id(每次登录该Id自动生成)
        /// </summary>
        public string LoginId { get; set; } = null!;
        /// <summary>
        /// 租户编号
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 自定义数据
        /// </summary>
        ///<remarks>
        ///可以在登录后放入一些数据（注意不能放入敏感数据，前端能够看到）
        /// </remarks>
        public string? CustomData { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string? ClientName { get; set; }
        /// <summary>
        /// 客户端版本
        /// </summary>
        public string? ClientVersion { get; set; }
    }
}
