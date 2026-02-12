// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.Module
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class SystemModuleDto : BaseDtoEmpty<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 越小越靠前
        /// </remarks>
        public int Order { get; set; } = 100;

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = "Gardener";


        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "0.0.1";

        /// <summary>
        /// 作者页面地址
        /// </summary>
        public string? AuthorHome { get; set; } = "https://gitee.com/hgflydream/Gardener";

        /// <summary>
        /// 运行中的模块信息
        /// </summary>
        [NotMapped]
        public SystemModuleDto? RuningModule { get; set; }
    }
}
