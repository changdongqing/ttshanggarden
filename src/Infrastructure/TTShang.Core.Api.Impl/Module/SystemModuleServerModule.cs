// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Module.Services;
using TTShang.Core.Module;

namespace TTShang.Core.Api.Impl.Module
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SystemModuleServerModule : SystemModuleModule, IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

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
                return [typeof(SystemModuleService)];
            }
        }

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            return new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="appstore",
       Id=new Guid("133bd614-db31-4ba5-a74a-8525f258f39c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_modules",
       ModuleName="SystemModule",
       Name="模块",
       Order=110,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_manager/modules",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b9aee527-3cff-0f61-5c53-32f361d3c34b"),
           ModuleName="SystemModule",
           ResourceId=new Guid("133bd614-db31-4ba5-a74a-8525f258f39c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }

};
        }
    }
}
