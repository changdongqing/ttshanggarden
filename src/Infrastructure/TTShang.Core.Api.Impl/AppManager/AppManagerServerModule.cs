// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.AppManager.Services;
using TTShang.Core.AppManager;
using TTShang.Core.Module;

namespace TTShang.Core.Api.Impl.AppManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AppManagerServerModule : AppManagerModule, IServerModule
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
                return [typeof(AppService), typeof(AppVersionService)];
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            return new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="windows",
       Id=new Guid("f3601cd5-1ee6-488c-85aa-ac7cf76a3ffc"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager",
       ModuleName="AppManager",
       Name="应用管理",
       Order=500,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7d201b1d-6c1a-4685-b14b-c32da3c80695"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_add",
       ModuleName="AppManager",
       Name="添加",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("327bbe82-bc0e-7488-4821-d64d2ced4464"),
           ModuleName="AppManager",
           ResourceId=new Guid("7d201b1d-6c1a-4685-b14b-c32da3c80695"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("455b8c21-9bac-4c53-82df-130489463da0"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_delete",
       ModuleName="AppManager",
       Name="删除",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f1481996-1b26-d1ad-0397-1f10af4288ba"),
           ModuleName="AppManager",
           ResourceId=new Guid("455b8c21-9bac-4c53-82df-130489463da0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f8e012cc-f567-1c20-e8c4-d0860b91e9d6"),
           ModuleName="AppManager",
           ResourceId=new Guid("455b8c21-9bac-4c53-82df-130489463da0"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a800fd08-c0ff-4c87-9217-d9cbdd8d9a4f"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_delete_selected",
       ModuleName="AppManager",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5bbc2925-55dc-25a4-d5f5-62b7c0a7ea59"),
           ModuleName="AppManager",
           ResourceId=new Guid("a800fd08-c0ff-4c87-9217-d9cbdd8d9a4f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5f115442-b027-95f4-2a8a-3d017bffec06"),
           ModuleName="AppManager",
           ResourceId=new Guid("a800fd08-c0ff-4c87-9217-d9cbdd8d9a4f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("15614a7a-b01e-4c3f-be35-216515beac85"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_detail",
       ModuleName="AppManager",
       Name="详情",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d081cc0d-240a-9c79-ff71-986ac0f8d21f"),
           ModuleName="AppManager",
           ResourceId=new Guid("15614a7a-b01e-4c3f-be35-216515beac85"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_index",
       ModuleName="AppManager",
       Name="应用",
       Order=0,
       ParentId=new Guid("f3601cd5-1ee6-488c-85aa-ac7cf76a3ffc"),
       Path="/app-manager/app",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("187d1ad9-79cf-4cc3-ac5d-3f4d93ee889d"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_lock",
       ModuleName="AppManager",
       Name="锁定",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6bb366f6-ffe5-f958-020a-2cf4fb1fd656"),
           ModuleName="AppManager",
           ResourceId=new Guid("187d1ad9-79cf-4cc3-ac5d-3f4d93ee889d"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("16ee890f-8f9b-4ace-bde9-36f9ddad4d25"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_refresh",
       ModuleName="AppManager",
       Name="刷新",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("de3f66bd-6d82-7735-5c6f-c15fba351ddd"),
           ModuleName="AppManager",
           ResourceId=new Guid("16ee890f-8f9b-4ace-bde9-36f9ddad4d25"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cca99061-7c5f-4720-813a-666dce11c2b5"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_search",
       ModuleName="AppManager",
       Name="搜索",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("de3f66bd-6d82-7735-5c6f-c15fba351ddd"),
           ModuleName="AppManager",
           ResourceId=new Guid("cca99061-7c5f-4720-813a-666dce11c2b5"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("60262569-f941-42be-a828-cdcd81345d7b"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_update",
       ModuleName="AppManager",
       Name="更新",
       Order=0,
       ParentId=new Guid("f0e155fb-8d8e-4853-ae97-a70eb5c42954"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("18693a74-8a6e-ee77-f982-cc9325716291"),
           ModuleName="AppManager",
           ResourceId=new Guid("60262569-f941-42be-a828-cdcd81345d7b"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("27e2d89a-bc8f-4f36-a7ac-4f972e4180c5"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_add",
       ModuleName="AppManager",
       Name="添加",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f1acc8d7-0e71-027c-d98f-af0a529770ce"),
           ModuleName="AppManager",
           ResourceId=new Guid("27e2d89a-bc8f-4f36-a7ac-4f972e4180c5"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("64ec577d-1c1c-4e6e-982d-98a0c0f5c8a6"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_delete",
       ModuleName="AppManager",
       Name="删除",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("333d993a-64b0-25e2-6d7c-30fee6d3f207"),
           ModuleName="AppManager",
           ResourceId=new Guid("64ec577d-1c1c-4e6e-982d-98a0c0f5c8a6"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("894b394d-1230-5b6c-d523-fc6919598f86"),
           ModuleName="AppManager",
           ResourceId=new Guid("64ec577d-1c1c-4e6e-982d-98a0c0f5c8a6"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("69826b88-64a0-4e7f-967e-81ec86e64011"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_delete_selected",
       ModuleName="AppManager",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7113c43b-4386-c0e9-a0fe-69a2b63a8900"),
           ModuleName="AppManager",
           ResourceId=new Guid("69826b88-64a0-4e7f-967e-81ec86e64011"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("87346981-b200-ee23-fd15-f59cbdf6142b"),
           ModuleName="AppManager",
           ResourceId=new Guid("69826b88-64a0-4e7f-967e-81ec86e64011"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ea67bd37-59a3-4bca-883b-91e6d83fb7d7"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_detail",
       ModuleName="AppManager",
       Name="详情",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0f94c8b5-5494-f2cc-fd02-407d70591de1"),
           ModuleName="AppManager",
           ResourceId=new Guid("ea67bd37-59a3-4bca-883b-91e6d83fb7d7"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_index",
       ModuleName="AppManager",
       Name="应用版本",
       Order=10,
       ParentId=new Guid("f3601cd5-1ee6-488c-85aa-ac7cf76a3ffc"),
       Path="/app-manager/app-version",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5dd1ca25-c5e5-4526-913e-afc4e57b8edc"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_lock",
       ModuleName="AppManager",
       Name="锁定",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5419f5d4-234d-f8b2-419c-fcb64e1f677a"),
           ModuleName="AppManager",
           ResourceId=new Guid("5dd1ca25-c5e5-4526-913e-afc4e57b8edc"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cb343b6a-aba3-41ad-8698-b7e8e1bd17b4"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_refresh",
       ModuleName="AppManager",
       Name="刷新",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2f6ca212-8872-c4f8-38a6-086c8b3af2a0"),
           ModuleName="AppManager",
           ResourceId=new Guid("cb343b6a-aba3-41ad-8698-b7e8e1bd17b4"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1cb1e715-c4a7-4b29-a028-0417717df1da"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_search",
       ModuleName="AppManager",
       Name="搜索",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2f6ca212-8872-c4f8-38a6-086c8b3af2a0"),
           ModuleName="AppManager",
           ResourceId=new Guid("1cb1e715-c4a7-4b29-a028-0417717df1da"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("41a4c24b-1e10-4359-9e2a-56ac24ff3a2e"),
       IsDeleted=false,
       IsLocked=false,
       Key="app_manager_app_version_update",
       ModuleName="AppManager",
       Name="更新",
       Order=0,
       ParentId=new Guid("db52cc06-52de-4c97-b1aa-58766317fa23"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e5690941-6856-07f9-dffc-2d3aaa990bd7"),
           ModuleName="AppManager",
           ResourceId=new Guid("41a4c24b-1e10-4359-9e2a-56ac24ff3a2e"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

};
        }
    }
}
