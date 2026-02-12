// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Module
{
    /// <summary>
    /// 模块
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get
            {
                return "8.0.1";
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return "Gardener";
            }
        }

        /// <summary>
        /// 作者页面地址
        /// </summary>
        public string? AuthorHome
        {
            get
            {
                return "https://gitee.com/hgflydream/Gardener";
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 越小越靠前
        /// </remarks>
        public int Order { get { return 1000; } }

        /// <summary>
        /// 自动安装
        /// </summary>
        public bool AutoInstall { get { return true; } }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get { return null; } }

        
        /// <summary>
        /// 当进程启动时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 尽量快
        /// </remarks>
        /// <returns></returns>
        Task OnStart(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当进程启动后
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 可以慢一点
        /// </remarks>
        /// <returns></returns>
        Task OnExecute(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当进程结束时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 尽量快
        /// </remarks>
        /// <returns></returns>
        Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当安装时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnInstall(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当卸载时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnUninstall(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 获取本地化器类型
        /// </summary>
        /// <returns>获取本地化器类型</returns>
        Type? GetLocalizationLocalizerType()
        {
            return null;
        }

        /// <summary>
        /// 自定义异常code类型
        /// </summary>
        /// <remarks>
        /// <para>类型中的字段可以作为错误码,常用枚举类型。</para>
        /// <para>字段使用[ErrorCodeItemMetadataAttribute]时可以指定code和message，优先级高</para>
        /// <para>字段使用<see cref="System.ComponentModel.DescriptionAttribute"/>时，以字段名作为code，以Description作为message，优先级次之</para>
        /// <para>字段无特性标记时，code和message都是字段名。</para>
        /// </remarks>
        /// <returns></returns>
        Type[]? GetCustomErrorCodeTypes()
        {
            return null;
        }


    }
}
