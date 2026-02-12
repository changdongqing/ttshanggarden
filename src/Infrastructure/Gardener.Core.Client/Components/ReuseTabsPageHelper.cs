// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Gardener.Core.Client.Components
{
    /// <summary>
    /// 多标签帮助类
    /// </summary>
    public static class ReuseTabsPageHelper
    {
        /// <summary>
        /// tabs 页面title占位符
        /// </summary>
        public static readonly string ReuseTabsPageTitlePlaceholder = "$Title$";
        /// <summary>
        /// tabs 页面title 格式化参数名
        /// </summary>
        public static readonly string ReuseTabsPageTitleFormateParameterName = "ReuseTabsPageTitleFormate";
        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="navigationManager"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title
        /// </remarks>
        public static string GetPageTitleValue(Type pageType, NavigationManager? navigationManager = null)
        {
            string title = "";
            RouteAttribute? routeAttribute = pageType.GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem? menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name ?? "";
            }

            if (navigationManager != null)
            {
                //从url加载title控制参数
                var url = new Uri(navigationManager.Uri);
                var query = url.Query;
                Dictionary<string, StringValues> urlParams = QueryHelpers.ParseQuery(query);
                if (urlParams != null && urlParams.Count() > 0)
                {
                    if (urlParams.ContainsKey(ReuseTabsPageTitleFormateParameterName))
                    {
                        StringValues formates = urlParams[ReuseTabsPageTitleFormateParameterName];
                        string? formate = formates.First();
                        if (formate != null)
                        {
                            title = formate.Replace(ReuseTabsPageTitlePlaceholder, title);
                        }
                    }
                }
            }
            return title;
        }
        /// <summary>
        /// 创建tabsUrl构建器
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ReuseTabsUrlBuilder CreateTabsUrlBuilder(string url)
        {
            return new ReuseTabsUrlBuilder(url);
        }
        /// <summary>
        /// 获取URL参数
        /// </summary>
        /// <param name="navigationManager"></param>
        /// <returns></returns>
        public static Dictionary<string, StringValues> GetQueryParams(NavigationManager navigationManager)
        {
            //从url加载title控制参数
            var url = new Uri(navigationManager.Uri);
            var query = url.Query;
            Dictionary<string, StringValues> urlParams = QueryHelpers.ParseQuery(query);
            return urlParams;
        }
    }
    /// <summary>
    /// tabsUrl构建器
    /// </summary>
    public class ReuseTabsUrlBuilder
    {
        private string url;
        private List<KeyValuePair<string, string?>> querys = new List<KeyValuePair<string, string?>>();
        private Func<string, string>? formatTitle;
        public ReuseTabsUrlBuilder(string url)
        {
            this.url = url;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        public ReuseTabsUrlBuilder(Type pageType)
        {
            RouteAttribute? routeAttribute = pageType.GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                this.url = routeAttribute.Template;
            }
            else
            {
                throw new InvalidOperationException($"{pageType.Name} not find RouteAttribute");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatTitle"></param>
        /// <returns></returns>
        public ReuseTabsUrlBuilder FormatTitle(Func<string, string> formatTitle)
        {
            this.formatTitle = formatTitle;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReuseTabsUrlBuilder AddParameter(string key, object? value)
        {
            querys.Add(new KeyValuePair<string, string?>(key, value?.ToString()));
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            string urlResult = QueryHelpers.AddQueryString(url, querys);
            if (formatTitle != null)
            {
                urlResult = QueryHelpers.AddQueryString(urlResult, ReuseTabsPageHelper.ReuseTabsPageTitleFormateParameterName, formatTitle(ReuseTabsPageHelper.ReuseTabsPageTitlePlaceholder));
            }
            return urlResult;
        }
    }
}
