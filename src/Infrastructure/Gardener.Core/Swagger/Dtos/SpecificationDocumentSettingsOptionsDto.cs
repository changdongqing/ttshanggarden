// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Swagger.Dtos
{
    /// <summary>
    /// swagger 文档分组信息
    /// </summary>
    public class SwaggerSpecificationOpenApiInfoDto
    {
        /// <summary>
        /// 所属组
        /// </summary>
        public required string Group { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string? Description { get; set; }
    }
}