// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Api.Impl.Common
{
    /// <summary>
    /// 公共模块
    /// </summary>
    /// <remarks>
    /// 配置一些基础公共模块信息
    /// </remarks>
    public class CommonServerModule : CommonModule, IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type[]? GetCustomErrorCodeTypes()
        {
            return [typeof(ExceptionCode)];
        }

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            return
            [new ResourceDto()
    {
       Hide=false,
       Icon="apartment",
       Id=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       IsDeleted=false,
       IsLocked=false,
       Key="admin_root",
       Name="后台根节点",
       Order=0,
       Path="",
       Remark="根根节点不能删除，不能改变类型！！。",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("10afa3d8-2e9b-63f2-2cec-c99bfe42a452"),
           ResourceId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Root,
    }
, new ResourceDto()
    {
       Hide=false,
       Icon="apartment",
       Id=new Guid("f4239a53-b5e1-49bd-99c6-967a86f07cdc"),
       IsDeleted=false,
       IsLocked=false,
       Key="front_root",
       Name="前台根节点",
       Order=1,
       Path="",
       Remark="根根节点不能删除，不能改变类型！！。",
       SupportMultiTenant=true,
       Type=ResourceType.Root,
    },
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_login",
       ModuleName="Common",
       Name="登录",
       Order=0,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       Path="",
       Remark="登录系统",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0b6bde9e-19eb-f0ab-20fc-c830d1fc2b98"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("20017bbd-fefe-d7f3-4343-fac4fe00028f"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("35891c81-89c3-6ea5-9095-8ecfd44d9e14"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("797a8ea4-7887-c886-66b4-4fcf77369bc0"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8096d344-eeb2-1f32-f434-6badbd524e87"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f0f58308-39b5-b502-5055-a4db35fcadf2"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f58e1b81-3a1b-8418-1fd8-a34dc623df14"),
           ModuleName="Common",
           ResourceId=new Guid("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    },
                new ResourceDto()
                {
                Hide=false,
                Icon="home",
                Id=new Guid("371b335b-29e5-4846-b6de-78c9cc691717"),
                IsDeleted=false,
                IsLocked=false,
                Key="admin_home",
                ModuleName="Common",
                Name="首页",
                Order=10,
                ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
                Path="/",
                Remark="",
                SupportMultiTenant=true,
                Type=ResourceType.Menu,
                },
                new ResourceDto()
                {
                Hide=false,
                Icon="setting",
                Id=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
                IsDeleted=false,
                IsLocked=false,
                Key="system_manager",
                ModuleName="Common",
                Name="系统管理",
                Order=20,
                ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
                Path="",
                Remark="系统管理",
                SupportMultiTenant=true,
                Type=ResourceType.Menu,
                }
            ];
        }
    }
}
