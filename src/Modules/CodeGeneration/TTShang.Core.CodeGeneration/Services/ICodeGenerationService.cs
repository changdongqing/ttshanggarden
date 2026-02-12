// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.CodeGeneration.Dtos;

namespace TTShang.Core.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务
    /// </summary>
    public interface ICodeGenerationService
    {
        /// <summary>
        /// 获取实体描述列表
        /// </summary>
        /// <remarks>
        /// 获取所有实体类描述信息列表
        /// </remarks>
        /// <returns></returns>
        Task<IEnumerable<EntityDescriptionDto>> GetEntityDescriptions();

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <remarks>
        /// 根据模板和实体类信息生成代码
        /// </remarks>
        /// <param name="generateCodeInput"></param>
        /// <returns></returns>
        Task<string> GenerationCode(GenerateCodeInput generateCodeInput);
    }
}
