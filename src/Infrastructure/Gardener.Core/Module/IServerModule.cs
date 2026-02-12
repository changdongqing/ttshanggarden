// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Dtos;

namespace Gardener.Core.Module
{
    /// <summary>
    /// 服务端模块
    /// </summary>
    public interface IServerModule : IModule
    {
        /// <summary>
        /// 自动注册接口
        /// </summary>
        public bool AutoRegisterFunction { get { return true; } }

        /// <summary>
        /// 接口分组名称-默认和模块名一致
        /// </summary>
        public string ApiGroupName
        {
            get
            {
                return Name;
            }
        }
        /// <summary>
        /// 限定模块下Api ControlType
        /// </summary>
        /// <remarks>
        /// 如果同一<see cref="ApiGroupName"/>下有多个模块，请限制要自动写入function的api的ControlType,否则将出现重复扫描。
        /// </remarks>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <returns></returns>
        FunctionDto[]? RegisterFunction()
        {
            return null;
        }

        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        CodeTypeDto[]? RegisterDic()
        {
            return null;
        }

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        ResourceDto[]? RegisterResource()
        {
            return null;
        }
        /// <summary>
        /// 注册租户配置模板
        /// </summary>
        /// <returns></returns>
        SystemTenantConfigTemplateDto[]? RegisterTenantConfigTemplate()
        { 
            return null ;
        }
    }
}
