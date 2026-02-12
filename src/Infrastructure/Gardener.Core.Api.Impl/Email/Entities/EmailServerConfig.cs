using Gardener.Core.Email.Dtos;
using Gardener.Core.Email.Enums;

namespace Gardener.Core.Api.Impl.Email.Entities
{
    /// <summary>
    /// 邮件服务器配置信息
    /// </summary>
    public class EmailServerConfig : EmailServerConfigDto, IEntityBase, IEntitySeedData<EmailServerConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<EmailServerConfig> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new EmailServerConfig{
                Id=new Guid("1812E5C1-7BCC-4D51-9B5E-45D610357E0E"),
                Name="QQ Email",
                Remark="QQ Email",
                Host="smtp.qq.com",
                Port=25,
                FromEmail="888888@qq.com",
                AccountName="888888@qq.com",
                AccountPassword="123456",
                Tags=$"{EmailServerTag.Default},{EmailServerTag.QQ}",
                IsLocked=false,
                IsDeleted=false,
                EnableSsl=false,
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636428834)
                }

            };
        }
    }
}
