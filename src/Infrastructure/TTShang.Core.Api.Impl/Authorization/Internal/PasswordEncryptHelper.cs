// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Api.Impl.Authorization.Internal
{
    /// <summary>
    /// 加密密码
    /// </summary>
    internal class PasswordEncryptHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string Encrypt(string password, string encryptKey)
        {
            var encryptedPassword = MD5Helper.Encrypt(password + encryptKey);
            return encryptedPassword;
        }
    }
}
