// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;
using TTShang.Core.Dict.Services;

namespace TTShang.Core.Client.Impl.Dict.Services
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
