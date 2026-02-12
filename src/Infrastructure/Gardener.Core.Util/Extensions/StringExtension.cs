// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Humanizer;
using System.Text.RegularExpressions;

namespace Gardener.Core.Util.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        #region Camel - Humanizer
        // https://github.com/Humanizr/Humanizer

        /// <summary>
        /// "some_title for" => "someTitleFor", 小驼峰
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToLowerCamel(this string input)
        {
            return input.Camelize();
        }

        /// <summary>
        /// "some_title for" => "SomeTitleFor", 大驼峰
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToUpperCamel(this string input)
        {
            return input.Pascalize();
        }
        /// <summary>
        /// 小驼峰转下划线分割
        /// </summary>
        /// <param name="camelCaseString"></param>
        /// <returns></returns>
        public static string ToUnderscore(this string camelCaseString)
        {
            return camelCaseString.Underscore();
        }

        /// <summary>
        /// 替换下滑线为中横线
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string Dasherize(this string underscoredWord)
        {
            return underscoredWord.Replace('_', '-');
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstToLower(this string input)
        {
            return input.First().ToString().ToLower() + input.Substring(1);
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        public static bool IsNumber(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(str);
        }

        // End
        /// <summary>
        /// hex转byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexToByte(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
    }
}
