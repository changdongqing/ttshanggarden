// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.Reflection;

namespace TTShang.Core.Util.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get Enum Code
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCode(this Enum input)
        {
            return input.ToString();
        }

        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum input)
        {
            if (input == null)
                return "";

            FieldInfo? fieldInfo = input.GetType().GetField(input.ToString());
            if (fieldInfo == null)
                return string.Empty;

            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
                return input.ToString();
            else
                return ((DescriptionAttribute)attribArray[0]).Description;
        }

        /// <summary>
        /// 获取描述特性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string? GetEnumDescription<T>(this T t) where T : Enum
        {
            object[]? attrs = t.GetType().GetField(t.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), true); ;
            if (attrs != null && attrs.Length > 0)
            {
                DescriptionAttribute? descAttr = attrs[0] as DescriptionAttribute;
                return descAttr?.Description;
            }
            return null;
        }

        /// <summary>
        /// 获取描述特性值为null时返回名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetEnumDescriptionOrName<T>(this T t) where T : Enum
        {
            string? desc = null;
            object[]? attrs = t.GetType().GetField(t.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), true); ;
            if (attrs != null && attrs.Length > 0)
            {
                DescriptionAttribute? descAttr = attrs[0] as DescriptionAttribute;
                desc = descAttr?.Description;
            }
            return desc ?? t.ToString();
        }
    }
}
