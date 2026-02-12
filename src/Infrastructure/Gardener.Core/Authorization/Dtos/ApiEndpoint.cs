// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------



using Mapster;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Gardener.Core.Authorization.Dtos
{
    /// <summary>
    /// api终结点
    /// </summary>
    public class ApiEndpoint
    {
        static ApiEndpoint()
        {
            //配置mapster 指定构造函数，不适用空的构造函数
            TypeAdapterConfig<ApiEndpoint, ApiEndpoint>
                .NewConfig()
                .ConstructUsing(src => new ApiEndpoint(src.Key, src.Path,src.Method,src.Group));
        }
        /// <summary>
        /// api终结点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="group"></param>
        public ApiEndpoint(string key, string path, ApiHttpMethod method, string group)
        {
            Key = key;
            Path = path;
            Method = method;
            Group = group;
        }
        /// <summary>
        /// 控制器类型
        /// </summary>
        [JsonIgnore]
        public TypeInfo? ControllerTypeInfo { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        public ApiHttpMethod Method { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string? GroupTitle { get; set; }

        /// <summary>
        /// 分组描述
        /// </summary>
        public string? GroupDescription { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

    }
}
