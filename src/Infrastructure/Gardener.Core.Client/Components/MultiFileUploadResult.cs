// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Components
{

    /// <summary>
    /// 多文件上传结果
    /// </summary>
    public class MultiFileUploadResult
    {
        /// <summary>
        /// 已传文件列表
        /// </summary>
        public List<UploadFileItem> FileList { get; set; }
        /// <summary>
        /// 多文件上传结果
        /// </summary>
        /// <param name="fileList"></param>
        public MultiFileUploadResult(List<UploadFileItem> fileList)
        {
            FileList = fileList;
        }
    }
}
