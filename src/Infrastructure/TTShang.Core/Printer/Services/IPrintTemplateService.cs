// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Services
{
    /// <summary>
    /// 打印模板服务
    /// </summary>
    public interface IPrintTemplateService : IServiceBase<PrintTemplateDto, Guid>
    {
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<PrintTemplateDto?> GetTemplateByKey(string key);

        /// <summary>
        /// 转换成指令
        /// </summary>
        /// <param name="key">模板键</param>
        /// <param name="model">数据对象</param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames">命名空间</param>
        /// <returns></returns>
        Task<List<PrintCommand>?> Convert(string key, object model, string[]? assemblyNames = null, params string[] usingNames);

        /// <summary>
        /// 转换成对应协议的byte[]
        /// </summary>
        /// <param name="key"></param>
        /// <param name="protocolType"></param>
        /// <param name="model"></param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames"></param>
        /// <returns></returns>
        Task<byte[]> ConvertToByte(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames);
        /// <summary>
        /// 转换成对应协议的Base64字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="protocolType"></param>
        /// <param name="model"></param>
        /// <param name="assemblyNames"></param>
        /// <param name="usingNames"></param>
        /// <returns></returns>
        Task<string?> ConvertToBase64(string key, PrintProtocolType protocolType, object model, string[]? assemblyNames = null, params string[] usingNames);

    }
}
