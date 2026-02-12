// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Primitives;

namespace TTShang.Core.Client.Components.PageBaseClass
{
    /// <summary>
    /// tab基类 (可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    public abstract class ReuseTabsPageAndFormBase<TSelfOperationDialogInput, TSelfOperationDialogOutput>
        : FeedbackComponent<TSelfOperationDialogInput, TSelfOperationDialogOutput>, IReuseTabsPage
    {
        /// <summary>
        /// 路由导航服务
        /// </summary>
        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// 弹框区域的加载中标识
        /// </summary>
        protected ClientLoading _dialogLoading = new ClientLoading();

        /// <summary>
        /// Page start loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected Task StartLoading(bool forceRender = false)
        {
            var run = _dialogLoading.Start();
            if (run && forceRender)
            {
                return InvokeAsync(StateHasChanged);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Page stop loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected Task StopLoading(bool forceRender = false)
        {
            var stop = _dialogLoading.Stop();
            if (stop && forceRender)
            {
                return InvokeAsync(StateHasChanged);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 只有当被当做页面打开时有效
        /// </remarks>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title，只有当被当做页面打开时有效
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            return ReuseTabsPageHelper.GetPageTitleValue(GetType(), Navigation);
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public Task RefreshPageDom()
        {
            return InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 关闭自己
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public Task CloseAsync(TSelfOperationDialogOutput? output = default)
        {
            return FeedbackRef.CloseAsync(output);
        }

        /// <summary>
        /// 获取URL参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, StringValues> GetQueryParams()
        {
            return ReuseTabsPageHelper.GetQueryParams(Navigation);
        }
    }

    /// <summary>
    /// tab基类
    /// </summary>
    public abstract class ReuseTabsPageBase : ComponentBase, IReuseTabsPage
    {
        /// <summary>
        /// 路由导航服务
        /// </summary>
        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            return ReuseTabsPageHelper.GetPageTitleValue(GetType(), Navigation);
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public Task RefreshPageDom()
        {
            return InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 获取URL参数
        /// </summary>
        /// <param name="navigationManager"></param>
        /// <returns></returns>
        public Dictionary<string, StringValues> GetQueryParams(NavigationManager navigationManager)
        {
            return ReuseTabsPageHelper.GetQueryParams(navigationManager);
        }
    }
}
