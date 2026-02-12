// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.CodeGeneration.Dtos
{
    /// <summary>
    /// 生成代码输入
    /// </summary>
    public class GenerateCodeInput
    {
        /// <summary>
        /// 生成代码输入
        /// </summary>
        /// <param name="templateContent"></param>
        /// <param name="entityTypeFullName"></param>
        public GenerateCodeInput(string templateContent, string entityTypeFullName)
        {
            TemplateContent = templateContent;
            EntityTypeFullName = entityTypeFullName;
        }

        /// <summary>
        /// 模板
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 实体类名
        /// </summary>
        public string EntityTypeFullName { get; set; }
    }
}
