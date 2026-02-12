// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment.Enums;
using TTShang.Core.Module;

namespace TTShang.Core.Attachment
{
    /// <summary>
    /// 模块
    /// </summary>
    public class AttachmentModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Attachment";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "附件模块";
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 180;

        /// <summary>
        /// 自定义异常code类型
        /// </summary>
        /// <returns></returns>
        public Type[]? GetCustomErrorCodeTypes()
        {
            return [typeof(AttachmentExceptionCode)];
        }
    }
}
