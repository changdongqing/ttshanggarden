// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// 东方财富实时新闻事件
    /// </summary>
    public class DongFangCaiFuNewsEvent : EventBase
    {
        /// <summary>
        /// 东方财富实时新闻事件
        /// </summary>
        public DongFangCaiFuNewsEvent() : base()
        {
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string? Content { get; set; }
    }
}
