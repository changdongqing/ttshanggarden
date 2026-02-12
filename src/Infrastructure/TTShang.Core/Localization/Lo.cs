// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Localization;

namespace TTShang.Core.Localization
{
    /// <summary>
    /// 静态本地化器
    /// </summary>
    public static class Lo
    {

        private static Func<Type, ILocalizationLocalizer?>? localizerProvider;

        /// <summary>
        /// 设置服务仓库
        /// </summary>
        /// <param name="localizerProvider"></param>
        public static void Init(Func<Type, ILocalizationLocalizer?> localizerProvider)
        {
            Lo.localizerProvider = localizerProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">资源文件类</typeparam>
        /// <returns></returns>
        public static ILocalizationLocalizer? GetService<T>()
        {
            return GetService(typeof(T));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">本地化器类型</param>
        /// <returns></returns>
        public static ILocalizationLocalizer? GetService(Type type)
        {
            if (localizerProvider == null)
            {
                return default;
            }
            ILocalizationLocalizer? service = localizerProvider?.Invoke(type);
            return service;
        }
        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(params string[] names)
        {
            return Combination(localizerProvider?.Invoke(typeof(ILocalizationLocalizer)), names);
        }
        /// <summary>
        /// 合并多个
        /// </summary>
        /// <typeparam name="T">资源文件类</typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination<T>(params string[] names)
        {
            ILocalizationLocalizer? l = GetService<ILocalizationLocalizer<T>>();
            return Combination(l, names);
        }
        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="type">本地化器类型</param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(Type type, params string[] names)
        {
            ILocalizationLocalizer? l = GetService(type);
            return Combination(l, names);
        }
        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(ILocalizationLocalizer? localizer, params string[] names)
        {
            if (localizer != null)
            {
                return localizer.Combination(names);
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(string name, bool toLower = false)
        {
            return GetValue(localizerProvider?.Invoke(typeof(ILocalizationLocalizer)), name, toLower);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue<T>(string name, bool toLower = false)
        {
            ILocalizationLocalizer? l = GetService<ILocalizationLocalizer<T>>();
            return GetValue(l, name, toLower);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="type">本地化器类型</param>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(Type type, string name, bool toLower = false)
        {
            ILocalizationLocalizer? l = GetService(type);
            return GetValue(l, name, toLower);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(ILocalizationLocalizer? localizer, string name, bool toLower = false)
        {
            if (localizer != null && !string.IsNullOrEmpty(name))
            {
                if (toLower)
                {
                    return localizer[name].ToLower();
                }
                return localizer[name];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LocalizedString Get(string name)
        {
            return Get(localizerProvider?.Invoke(typeof(ILocalizationLocalizer)), name);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LocalizedString Get<T>(string name)
        {
            ILocalizationLocalizer? l = GetService<ILocalizationLocalizer<T>>();
            return Get(l, name);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="type">本地化器类型</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LocalizedString Get(Type type, string name)
        {
            ILocalizationLocalizer? l = GetService(type);
            return Get(l, name);
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LocalizedString Get(ILocalizationLocalizer? localizer, string name)
        {
            if (localizer != null && !string.IsNullOrEmpty(name))
            {
                LocalizedString localizedString = localizer.Get(name);

                return localizedString;
            }
            return new LocalizedString(name, name, true);
        }
    }
}
