// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Components
{
    /// <summary>
    /// 上传按钮款式
    /// </summary>
    public enum UploadBtnStyle
    {
        Button,
        BlockText
    }
    /// <summary>
    /// 上传列表款式
    /// </summary>
    public sealed class UploadListType
    {
        private UploadListType(string type)
        {
            this.Type = type;
        }

        public string Type { get; init; }
        public static UploadListType Text = new UploadListType("text");
        public static UploadListType Picture = new UploadListType("picture");
        public static UploadListType PictureCard = new UploadListType("picture-card");
    }
    /// <summary>
    /// 多文件上传参数
    /// </summary>
    public class MultiFileUploadParams
    {
        /// <summary>
        /// 多文件上传参数
        /// </summary>
        /// <remarks>
        /// 如果不设置 <paramref name="uploadPath"/> 默认使用上传附件api
        /// </remarks>
        /// <param name="uploadPath"></param>
        public MultiFileUploadParams(string? uploadPath = null)
        {
            this.UploadPath = uploadPath;
        }
        /// <summary>
        /// 多文件上传参数-上传附件
        /// </summary>
        /// <param name="businessId">业务唯一编号</param>
        /// <param name="attachmentBusinessType">附件业务类型</param>
        /// <param name="maxFileNumber">最大文件数量:小于等于0时不限制</param>
        /// <param name="saveOriginalName">保存为原始名称</param>
        /// <param name="uploadFileTypes">限制文件类型[.jpg,.png]</param>
        /// <param name="fileMaxSize">限制文件最大长度：1024b</param>
        public MultiFileUploadParams(string businessId, AttachmentBusinessType attachmentBusinessType, int maxFileNumber, bool saveOriginalName = false, List<string>? uploadFileTypes = null, long? fileMaxSize = null) : this(() => businessId, attachmentBusinessType, maxFileNumber, saveOriginalName, uploadFileTypes, fileMaxSize)
        {
        }
        /// <summary>
        /// 多文件上传参数-上传附件
        /// </summary>
        /// <param name="businessIdProvider">业务唯一编号提供方法</param>
        /// <param name="attachmentBusinessType">附件业务类型</param>
        /// <param name="maxFileNumber">最大文件数量:小于等于0时不限制</param>
        /// <param name="saveOriginalName">保存为原始名称</param>
        /// <param name="uploadFileTypes">限制文件类型[.jpg,.png]</param>
        /// <param name="fileMaxSize">限制文件最大长度：1024b</param>
        public MultiFileUploadParams(Func<string> businessIdProvider, AttachmentBusinessType attachmentBusinessType, int maxFileNumber, bool saveOriginalName = false, List<string>? uploadFileTypes = null, long? fileMaxSize = null)
        {
            SetAttachmentBusiness(businessIdProvider, attachmentBusinessType, saveOriginalName);
            MaxFileNumber = maxFileNumber;
            UploadFileTypes = uploadFileTypes;
            FileMaxSize = fileMaxSize;
        }
        /// <summary>
        /// 设置上传附件附带参数
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="attachmentBusinessType"></param>
        /// <param name="saveOriginalName">保存为原始名称</param>
        /// <returns></returns>
        public MultiFileUploadParams SetAttachmentBusiness(string businessId, AttachmentBusinessType attachmentBusinessType, bool saveOriginalName = false)
        {
            SetAttachmentBusiness(() => businessId, attachmentBusinessType, saveOriginalName);
            return this;
        }
        /// <summary>
        /// 设置上传附件附带参数
        /// </summary>
        /// <param name="businessIdProvider"></param>
        /// <param name="attachmentBusinessType"></param>
        /// <param name="saveOriginalName">保存为原始名称</param>
        /// <returns></returns>
        public MultiFileUploadParams SetAttachmentBusiness(Func<string> businessIdProvider, AttachmentBusinessType attachmentBusinessType, bool saveOriginalName = false)
        {
            _businessIdProvider = businessIdProvider;
            AddOrUpdateUploadData(nameof(UploadAttachmentInput.BusinessType), attachmentBusinessType);
            AddOrUpdateUploadData(nameof(UploadAttachmentInput.SaveOriginalName), saveOriginalName);
            return this;
        }
        /// <summary>
        /// 业务编号提供方法
        /// </summary>
        public Func<string>? _businessIdProvider;
        /// <summary>
        /// 上传地址
        /// </summary>
        /// <remarks>
        /// 如果不设置 默认使用上传附件api 地址
        /// </remarks>
        public string? UploadPath { get; set; }
        /// <summary>
        /// 文件类型限制
        /// </summary>
        public List<string>? UploadFileTypes { get; set; }
        /// <summary>
        /// 文件最大限制(byte)
        /// </summary>
        public long? FileMaxSize { get; set; }
        /// <summary>
        /// 最大文件数:小于等于0时不限制
        /// </summary>
        public int MaxFileNumber { get; set; } = 10;
        /// <summary>
        /// 支持多选
        /// </summary>
        public bool Multiple { get; set; } = true;
        /// <summary>
        /// 是否显示上传按钮
        /// </summary>
        public bool ShowUploadBtn { get; set; } = true;
        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowRemoveBtn { get; set; } = true;
        /// <summary>
        /// 是否显示预览按钮
        /// </summary>
        public bool ShowPreviewBtn { get; set; } = true;
        /// <summary>
        /// 是否显示下载按钮
        /// </summary>
        public bool ShowDownloadBtn { get; set; } = true;
        /// <summary>
        /// 是否显示组件上传列表
        /// </summary>
        public bool ShowUploadList { get; set; } = true;
        /// <summary>
        /// 在移除时，是否立即移除服务器文件
        /// </summary>
        public bool RightAwayRemoveOnServer { get; set; } = true;
        /// <summary>
        /// 上传按钮款式
        /// </summary>
        public UploadBtnStyle UploadBtnStyle { get; set; } = UploadBtnStyle.Button;
        /// <summary>
        /// 列表样式
        /// </summary>
        public UploadListType UploadListType { get; set; } = UploadListType.Text;

        /// <summary>
        /// 上传附带参数
        /// </summary>
        public Dictionary<string, object> UploadData
        {
            get
            {
                if (_businessIdProvider != null)
                {
                    string businessId = _businessIdProvider.Invoke();
                    AddOrUpdateUploadData(nameof(UploadAttachmentInput.BusinessId), businessId);
                }

                return _uploadData;
            }
            set
            {
                _uploadData = value;
            }
        }
        private Dictionary<string, object> _uploadData = new Dictionary<string, object>();
        /// <summary>
        /// 添加或更新上传数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MultiFileUploadParams AddOrUpdateUploadData(string key, object value)
        {
            if (_uploadData.ContainsKey(key))
            {
                _uploadData[key] = value;
            }
            else
            {
                _uploadData.Add(key, value);
            }
            return this;
        }
    }
}
