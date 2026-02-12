// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;

namespace TTShang.Core.Client.Constants
{
    /// <summary>
    /// 客户端常量配置
    /// </summary>
    public class ClientConstant
    {
        /// <summary>
        /// 操作按钮大小
        /// </summary>
        public readonly static string OperationButtonSize = "default";

        /// <summary>
        /// 底部友链
        /// </summary>
        public readonly static LinkItem[] FooterLinks =
        {
                new LinkItem{ Key = "Furion", BlankTarget = true, Title = "Furion" ,Href="https://gitee.com/monksoul/Furion"},
                new LinkItem{ Key = "Ant Design Blazor", BlankTarget = true, Title = "Ant Design",Href="https://github.com/ant-design-blazor/ant-design-blazor"},
                new LinkItem{ Key = "Gardener", BlankTarget = true, Title = "Gardener",Href="https://gitee.com/hgflydream/Gardener"}
        };


        /// <summary>
        /// 默认的操作框配置
        /// </summary>
        /// <remarks>
        /// 每次都是新对象
        /// </remarks>
        public static OperationDialogSettings DefaultOperationDialogSettings
        {
            get
            {
                return new OperationDialogSettings
                {
                    Width = "500",
                    MaskClosable = true,
                    Closable = true,
                    ModalCentered = true,
                    DrawerPlacement = Placement.Right,
                    DialogType = OperationDialogType.Modal
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        //public readonly static int PageOperationDialogWidth = 1200;//1080;//960;
        /// <summary>
        /// 启用多标签
        /// </summary>
        public readonly static bool EnabledTabs = true;
        /// <summary>
        /// 默认表格大小
        /// </summary>
        /// <remarks>
        /// Default
        /// Middle
        /// Small
        /// </remarks>
        public readonly static TableSize DefaultTableSize = TableSize.Small;
        /// <summary>
        /// 启用界面设置抽屉
        /// </summary>
        public readonly static bool EnableSettingDrawer = false;
        /// <summary>
        /// 启用页面底部公共视图
        /// </summary>
        /// <remarks>
        /// 主要展示友链和版权信息
        /// </remarks>
        public readonly static bool EnableFooterView = false;
        /// <summary>
        /// 页面Table高度
        /// </summary>
        /// <remarks>
        /// Vh为页面可视高度百分比
        /// </remarks>
        public readonly static string PageTableHeight = "60Vh";
        /// <summary>
        /// Table分页器位置
        /// </summary>
        /// <remarks>
        /// "none", "topLeft", "topCenter", "topRight", "bottomLeft", "bottomCenter", "bottomRight"
        /// </remarks>
        public readonly static string TablePaginationPosition = "bottomRight";
        /// <summary>
        /// 本地化支持语言
        /// </summary>
        public readonly static string[] SupportedCultures = { "zh-CN", "en-US" };

        /// <summary>
        /// 本地化浏览器缓存key
        /// </summary>
        public readonly static string BlazorCultureKey = "BlazorCulture";

        /// <summary>
        /// 本地化默认语言
        /// </summary>
        public readonly static string DefaultCulture = "zh-CN";


        /// <summary>
        /// 时间显示格式化
        /// </summary>
        public readonly static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


        /// <summary>
        /// 时间显示格式化
        /// </summary>
        public readonly static string InputDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss zzz";

        /// <summary>
        /// 每页数据量大小
        /// </summary>
        public readonly static int PageSize = 15;

        /// <summary>
        /// 通知消息使用MessageBox最大长度超出时，使用通知框
        /// </summary>
        public readonly static int ClientNotifierUseMessageMaxLength = 20;

        /// <summary>
        /// 通知消息弹出时长
        /// </summary>
        public readonly static int ClientNotifierMessageDuration = 3;
        /// <summary>
        /// 表格列表删除使用真删除
        /// </summary>
        /// <remarks>
        /// <para>true 调用物理删除接口</para>
        /// <para>false 调用逻辑删除接口</para>
        /// </remarks>
        public readonly static bool TableListDeleteUseTrueDelete = true;

    }
}
