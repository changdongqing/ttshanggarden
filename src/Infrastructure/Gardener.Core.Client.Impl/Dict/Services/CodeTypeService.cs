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
    /// 字典类型
    /// </summary>
    [ScopedService]
    public class CodeTypeService : ClientServiceBase<CodeTypeDto>, ICodeTypeService
    {
        public CodeTypeService(IApiCaller apiCaller) : base(apiCaller, "code-type", "dict")
        {
        }

        public Task<Dictionary<int, IEnumerable<CodeDto>>> GetCodeDic(params int[] codeTypeIds)
        {
            List<KeyValuePair<string, object?>> queryString = new List<KeyValuePair<string, object?>>();
            foreach (var item in codeTypeIds)
            {
                queryString.Add(new KeyValuePair<string, object?>("codeTypeIds", item));
            }
            return base.apiCaller.GetAsync<Dictionary<int, IEnumerable<CodeDto>>>($"{base.baseUrl}/code-dic", queryString);
        }

        public Task<Dictionary<string, IEnumerable<CodeDto>>> GetCodeDicByValues(params string[] codeTypeValues)
        {
            List<KeyValuePair<string, object?>> queryString = new List<KeyValuePair<string, object?>>();
            foreach (var item in codeTypeValues)
            {
                queryString.Add(new KeyValuePair<string, object?>("codeTypeValues", item));
            }
            return base.apiCaller.GetAsync<Dictionary<string, IEnumerable<CodeDto>>>($"{base.baseUrl}/code-dic-by-values", queryString);
        }

        public Task<IEnumerable<CodeDto>> GetCodes(int codeTypeId)
        {
            return base.apiCaller.GetAsync<IEnumerable<CodeDto>>($"{base.baseUrl}/{codeTypeId}/codes");
        }

        public Task<IEnumerable<CodeDto>> GetCodesByValue(string codeTypeValue)
        {
            return base.apiCaller.GetAsync<IEnumerable<CodeDto>>($"{base.baseUrl}/{codeTypeValue}/codes-by-value");
        }

        public async Task<bool> RefreshDictHelperCache()
        {
            //服务端刷新
            var task1 = base.apiCaller.PostWithoutBodyAsync<bool>($"{base.baseUrl}/refresh-dict-helper-cache");
            //客户端刷新
            var task2 = GetCodeDicByValues();
            await task1;
            DictHelper.InitAllCode(await task2);
            return true;
        }
    }
}
