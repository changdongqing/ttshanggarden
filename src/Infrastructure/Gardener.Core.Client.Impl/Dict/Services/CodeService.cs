// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Dtos;
using Gardener.Core.Dict.Services;

namespace Gardener.Core.Client.Impl.Dict.Services
{
    /// <summary>
    /// 字典服务
    /// </summary>
    [ScopedService]
    public class CodeService : ClientServiceBase<CodeDto>, ICodeService
    {
        public CodeService(IApiCaller apiCaller) : base(apiCaller, "code", "dict")
        {
        }
    }
}
