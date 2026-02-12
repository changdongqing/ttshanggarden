// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.CodeGeneration.Client.Services
{
    [ScopedService]
    public class CodeGenerationService : ClientServiceCaller, ICodeGenerationService
    {
        public CodeGenerationService(IApiCaller apiCaller) : base(apiCaller, "code-generation", "code-gen")
        {
        }

        public Task<string> GenerationCode(GenerateCodeInput generateCodeInput)
        {
            return apiCaller.PostAsync<GenerateCodeInput, string>($"{base.baseUrl}/generation-code", generateCodeInput);
        }

        public Task<IEnumerable<EntityDescriptionDto>> GetEntityDescriptions()
        {
            return apiCaller.GetAsync<IEnumerable<EntityDescriptionDto>>($"{base.baseUrl}/entity-descriptions");
        }
    }
}
