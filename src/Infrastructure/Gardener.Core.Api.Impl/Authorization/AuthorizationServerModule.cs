// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Authorization.Services;
using Gardener.Core.Authorization;
using Gardener.Core.Module;

namespace Gardener.Core.Api.Impl.Authorization
{
    /// <summary>
    /// 模块
    /// </summary>
    public class AuthorizationServerModule : AuthorizationModule, IServerModule
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
                return [typeof(LoginTokenService), typeof(LoginLogService)];
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
       Id=new Guid("1b31bed8-0702-444a-9694-e165935ededd"),
       IsDeleted=false,
       IsLocked=false,
       Key="auth_login_log_detail",
       ModuleName="Authorization",
       Name="详情",
       Order=0,
       ParentId=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7fb69f46-1bca-d6a8-85f0-f9fbc85cb138"),
           ModuleName="Authorization",
           ResourceId=new Guid("1b31bed8-0702-444a-9694-e165935ededd"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_log",
       ModuleName="Authorization",
       Name="登录日志",
       Order=72,
       ParentId=new Guid("510adb5f-10f7-452a-8542-352fa5d79f63"),
       Path="/system_manager/login-log",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bfa2f737-a11b-49af-b8fc-6caebd93cc59"),
       IsDeleted=false,
       IsLocked=false,
       Key="auth_login_log_refresh",
       ModuleName="Authorization",
       Name="刷新",
       Order=0,
       ParentId=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("49a98cf5-c725-1b9b-55a6-c9638ce74e21"),
           ModuleName="Authorization",
           ResourceId=new Guid("bfa2f737-a11b-49af-b8fc-6caebd93cc59"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1cb5b768-c04d-44ff-ae76-052cc511f3f9"),
       IsDeleted=false,
       IsLocked=false,
       Key="auth_login_log_search",
       ModuleName="Authorization",
       Name="搜索",
       Order=0,
       ParentId=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("49a98cf5-c725-1b9b-55a6-c9638ce74e21"),
           ModuleName="Authorization",
           ResourceId=new Guid("1cb5b768-c04d-44ff-ae76-052cc511f3f9"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    },new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d62bf30c-57a9-4640-a627-fe6ec4eb9086"),
       IsDeleted=false,
       IsLocked=false,
       Key="auth_login_log_delete",
       ModuleName="Authorization",
       Name="删除",
       Order=0,
       ParentId=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c4fa952e-3418-0bf8-e370-2b8fdeedd8c5"),
           ModuleName="Authorization",
           ResourceId=new Guid("d62bf30c-57a9-4640-a627-fe6ec4eb9086"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f400284b-ba67-12e9-4782-6e26e82cbeb6"),
           ModuleName="Authorization",
           ResourceId=new Guid("d62bf30c-57a9-4640-a627-fe6ec4eb9086"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3f46b797-d77c-449d-af14-a36833719d2d"),
       IsDeleted=false,
       IsLocked=false,
       Key="auth_login_log_delete_selected",
       ModuleName="Authorization",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("5346050b-6f89-426c-934e-d2c665344866"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1c80f1cf-081f-789a-67c2-69a5db8f01e8"),
           ModuleName="Authorization",
           ResourceId=new Guid("3f46b797-d77c-449d-af14-a36833719d2d"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("58fd3941-68fb-b9f1-5359-d0f6317b7a6c"),
           ModuleName="Authorization",
           ResourceId=new Guid("3f46b797-d77c-449d-af14-a36833719d2d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="idcard",
       Id=new Guid("510adb5f-10f7-452a-8542-352fa5d79f63"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login",
       ModuleName="Authorization",
       Name="登录管理",
       Order=70,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token",
       ModuleName="Authorization",
       Name="Token管理",
       Order=70,
       ParentId=new Guid("510adb5f-10f7-452a-8542-352fa5d79f63"),
       Path="/system_manager/login-token",
       Remark="",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token_delete",
       ModuleName="Authorization",
       Name="删除登录Token",
       Order=0,
       ParentId=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0110747f-c8c3-73a4-7194-b8d8d007d089"),
           ModuleName="Authorization",
           ResourceId=new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e654dbda-cb91-4947-064d-36263775b799"),
           ModuleName="Authorization",
           ResourceId=new Guid("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token_delete_selected",
       ModuleName="Authorization",
       Name="删除选中登录Token",
       Order=0,
       ParentId=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6f1cd523-b6d6-50ca-fbde-1b6147d21b6a"),
           ModuleName="Authorization",
           ResourceId=new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b53f9361-a4c7-bd4a-8ae9-747e9171d314"),
           ModuleName="Authorization",
           ResourceId=new Guid("f077211f-0e79-44a3-935c-0f704f6a5962"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token_export",
       ModuleName="Authorization",
       Name="导出登录数据",
       Order=0,
       ParentId=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       Remark="导出登录数据",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1b4d3946-5cfc-633e-6922-88495264c7df"),
           ModuleName="Authorization",
           ResourceId=new Guid("bddc6ccc-3f93-4be7-8756-15613cdf76b6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token_lock",
       ModuleName="Authorization",
       Name="锁定登录Token",
       Order=0,
       ParentId=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0e703f8d-424d-f730-75e4-ec3b35c78322"),
           ModuleName="Authorization",
           ResourceId=new Guid("0cbb3d40-de41-483e-a76c-3d85682176af"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_login_token_refresh",
       ModuleName="Authorization",
       Name="刷新登录Token",
       Order=0,
       ParentId=new Guid("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("243ed3a9-ec17-82cd-821f-8047687e708e"),
           ModuleName="Authorization",
           ResourceId=new Guid("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
