// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.AppManager.Enums;

namespace TTShang.Core.AppManager.Dtos
{
    /// <summary>
    /// 应用客户端信息
    /// </summary>
    public class AppClientInfo
    {
        /// <summary>
        /// AppClientInfo
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="packageName"></param>
        /// <param name="currentVersionNumber"></param>
        /// <param name="currentVersioName"></param>
        public AppClientInfo(string appName, string packageName, long currentVersionNumber, string currentVersioName)
        {
            AppName = appName;
            PackageName = packageName;
            CurrentVersionNumber = currentVersionNumber;
            CurrentVersioName = currentVersioName;
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 应用包名
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 当前版本
        /// </summary>
        public long CurrentVersionNumber { get; set; }
        /// <summary>
        /// 当前版本名称
        /// </summary>
        public string CurrentVersioName { get; set; }
        /// <summary>
        /// 安装到本地
        /// </summary>
        public bool InstallLocal { get; set; } = true;
        /// <summary>
        /// 运行环境
        /// </summary>
        public AppEnvironments Environment { get; set; } = AppEnvironments.Develop;
        /// <summary>
        /// 是否离线运行
        /// </summary>
        public bool OfflineRunning { get; set; } = false;
        /// <summary>
        /// 主页地址
        /// </summary>
        public string HomePath { get; set; } = "/";

    }
}
