// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment;
using TTShang.Core.Attachment.Resources;
using TTShang.Core.Client.Module;

namespace TTShang.Core.Client.Impl.Attachment
{
    /// <summary>
    /// 附件客户端模块
    /// </summary>
    [SingletonService]
    public class AttachmentClientModule: AttachmentModule,IClientModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<AttachmentLocalResource>);
        }
    }
}
