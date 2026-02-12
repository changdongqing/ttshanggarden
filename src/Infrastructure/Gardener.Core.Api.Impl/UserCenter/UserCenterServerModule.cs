// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.UserCenter.Services;
using Gardener.Core.Dict.Dtos;
using Gardener.Core.Module;
using Gardener.Core.UserCenter;

namespace Gardener.Core.Api.Impl.UserCenter
{
    /// <summary>
    /// 模块
    /// </summary>
    public class UserCenterServerModule : UserCenterModule, IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

        /// <summary>
        /// 限定ApiTag
        /// </summary>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return [
                    typeof(AccountService),
                    typeof(ClientFunctionService),
                    typeof(ClientService),
                    typeof(DeptService),
                    typeof(PositionService),
                    typeof(RoleService),
                    typeof(TenantResourceService),
                    typeof(TenantService),
                    typeof(TenantConfigService),
                    typeof(TenantConfigTemplateService),
                    typeof(UserService)
                    ];
            }
        }

        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        public CodeTypeDto[]? RegisterDic()
        {
            return new CodeTypeDto[] {
                new CodeTypeDto() {CodeTypeName="岗位等级",CodeTypeValue="position-level",Remark="",IsLocked=false,IsDeleted=false,CreateBy="6",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:26:58"),UpdatedTime=DateTimeOffset.Parse("2023-04-25 09:27:06"),
                    Codes=new CodeDto[]
                    {
                        new CodeDto() {CodeTypeId=3,CodeValue="p1",CodeName="P1",Order=10,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:30:30"),},
                        new CodeDto() {CodeTypeId=3,CodeValue="p2",CodeName="P2",Order=20,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:30:50"),},
                        new CodeDto() {CodeTypeId=3,CodeValue="p3",CodeName="P3",Order=30,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:03"),},
                        new CodeDto() {CodeTypeId=3,CodeValue="p4",CodeName="P4",Order=40,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:22"),},
                        new CodeDto() {CodeTypeId=3,CodeValue="p5",CodeName="P5",Order=50,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:36"),},
                    }
                }
            };
        }

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            List<ResourceDto> result = new List<ResourceDto>();
            //个人中心
            result.AddRange(new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="user",
       Id=new Guid("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center",
       ModuleName="UserCenter",
       Name="个人中心",
       Order=99999,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("74a75b21-3fcf-4c26-b998-aa4f0b658292"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center_settings",
       ModuleName="UserCenter",
       Name="个人设置",
       Order=0,
       ParentId=new Guid("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),
       Path="/account/settings",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    },new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3da5b841-fad5-407d-b06f-912ba60dbe9d"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center_online_connection",
       ModuleName="UserCenter",
       Name="在线链接",
       Order=0,
       ParentId=new Guid("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),
       Path="/account/online-connection",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ef05768b-24a1-be88-f8db-7ffc889bf6f4"),
           ModuleName="UserCenter",
           ResourceId=new Guid("3da5b841-fad5-407d-b06f-912ba60dbe9d"),
        }
       }
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7a983726-92f2-4d47-9ee9-c15e279704d9"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center_settings_base",
       ModuleName="UserCenter",
       Name="基本信息",
       Order=0,
       ParentId=new Guid("74a75b21-3fcf-4c26-b998-aa4f0b658292"),
       ResourceFunctions=new[]{

            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03bde8d8-5b0c-bffa-7456-7c808676f815"),
           ModuleName="UserCenter",
           ResourceId=new Guid("7a983726-92f2-4d47-9ee9-c15e279704d9"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0ce1308a-aaef-2f02-7210-ecb3e69c489f"),
           ModuleName="UserCenter",
           ResourceId=new Guid("7a983726-92f2-4d47-9ee9-c15e279704d9"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9d549aeb-35fd-4345-849c-db85e42a103c"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center_settings_binding",
       ModuleName="UserCenter",
       Name="账号绑定",
       Order=0,
       ParentId=new Guid("74a75b21-3fcf-4c26-b998-aa4f0b658292"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("98c63bf2-fbc3-46d6-94dd-9c2a939b7ba6"),
       IsDeleted=false,
       IsLocked=false,
       Key="account_center_settings_security",
       ModuleName="UserCenter",
       Name="安全设置",
       Order=0,
       ParentId=new Guid("74a75b21-3fcf-4c26-b998-aa4f0b658292"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("95784ea0-7d99-cb7b-9136-6ecce781b5ef"),
           ModuleName="UserCenter",
           ResourceId=new Guid("98c63bf2-fbc3-46d6-94dd-9c2a939b7ba6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

});
            //用户中心
            result.AddRange(new[]{
                 new ResourceDto()
    {
        Id=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
        Name="租户配置",
        Key="user_center_tenant_config_index",
        ModuleName="UserCenter",
        Type=ResourceType.Menu,
        SupportMultiTenant=false,
        Path="/user_center/tenant-config",
        Icon="reconciliation",
        Order=2,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    }
,
    new ResourceDto()
    {
        Id=Guid.Parse("04e17d25-5320-449f-b3f2-9cba7f66b6c4"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="搜索",
        Key="user_center_tenant_config_search",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("350fac3c-8375-4744-8049-aace9baea414"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="刷新",
        Key="user_center_tenant_config_refresh",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("69bdcf60-1d02-4fb9-a974-9e4ef200276e"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="添加",
        Key="user_center_tenant_config_add",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("484ff0a8-3ac6-40bd-9693-b9305e70b93d"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="更新",
        Key="user_center_tenant_config_update",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("04305413-dcd4-4eee-b80b-3d09573300c3"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="详情",
        Key="user_center_tenant_config_detail",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("303b52a0-c559-4cd6-ac13-ef959445a99a"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="删除",
        Key="user_center_tenant_config_delete",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("1ef15bd7-1888-4191-bea8-71b71cf120ab"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="删除选中",
        Key="user_center_tenant_config_delete_selected",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
        Id=Guid.Parse("1791dc5d-f6a8-408d-bbb1-4eb2bae80886"),
        ParentId=Guid.Parse("d8a5737a-337b-40cc-99b9-84f98e281963"),
        Name="锁定",
        Key="user_center_tenant_config_lock",
        ModuleName="UserCenter",
        Type=ResourceType.Action,
        SupportMultiTenant=false,
        Path=null,
        Icon=null,
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy="1",
        CreateIdentityType=IdentityType.User
    },
    new ResourceDto()
    {
       Hide=false,
       Icon="cloud-server",
       Id=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client",
       ModuleName="UserCenter",
       Name="客户端管理",
       Order=45,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/system_manager/client",
       Remark="客户端管理",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_add",
       ModuleName="UserCenter",
       Name="添加客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="添加客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("413e1c55-c07a-4b37-5f08-ba3a22197da9"),
           ModuleName="UserCenter",
           ResourceId=new Guid("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),
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
       Id=new Guid("a1260e4c-e67c-4d72-a758-560a13e9c496"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_delete",
       ModuleName="UserCenter",
       Name="删除客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="删除客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1e35dccc-8d9b-065e-0bfe-6bc848eb2ac0"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a1260e4c-e67c-4d72-a758-560a13e9c496"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ece95eab-3779-23bf-cfa3-71e22a03aa4a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a1260e4c-e67c-4d72-a758-560a13e9c496"),
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
       Id=new Guid("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="删除选中客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0c8ed868-9f22-8779-e6bc-4ee093c69c39"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6d1c0bd6-3364-be05-400b-f39b0751093d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),
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
       Id=new Guid("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_detail",
       ModuleName="UserCenter",
       Name="查看客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="查看客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("83bd7635-54c4-8031-c6a1-f64a2a74635d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),
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
       Id=new Guid("92ed8299-ff26-4fae-b852-fe33f0c01a09"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_edit",
       ModuleName="UserCenter",
       Name="编辑客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="编辑客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cd1a8c2c-1cef-87df-48b2-f669b5739c31"),
           ModuleName="UserCenter",
           ResourceId=new Guid("92ed8299-ff26-4fae-b852-fe33f0c01a09"),
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
       Id=new Guid("a02edffb-0a63-4106-bac2-ea66f1f65060"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_function_add_page_show",
       ModuleName="UserCenter",
       Name="显示可选接口",
       Order=0,
       ParentId=new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),
       Path="",
       Remark="显示可选接口",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ed22e08c-0216-9809-de86-0dbaae260ab2"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a02edffb-0a63-4106-bac2-ea66f1f65060"),
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
       Id=new Guid("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_function_binding",
       ModuleName="UserCenter",
       Name="绑定客户端接口关系",
       Order=0,
       ParentId=new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),
       Path="",
       Remark="绑定资源接口关系",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5edbadb4-875d-bd60-0149-8632cc7580d1"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),
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
       Id=new Guid("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_function_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中客户端接口关系",
       Order=0,
       ParentId=new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),
       Path="",
       Remark="删除选中客户端接口关系",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("795f1316-3dad-71f9-d890-413b87534d44"),
           ModuleName="UserCenter",
           ResourceId=new Guid("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),
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
       Id=new Guid("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_refresh",
       ModuleName="UserCenter",
       Name="刷新客户端",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="刷新客户端",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c842e681-fb8e-f957-76bd-ded5b1ca0564"),
           ModuleName="UserCenter",
           ResourceId=new Guid("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),
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
       Id=new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_show_function",
       ModuleName="UserCenter",
       Name="关联客户端接口关系",
       Order=0,
       ParentId=new Guid("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),
       Path="",
       Remark="关联客户端接口关系",
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("86a086a1-0770-4df4-ade3-433ff7226399"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_client_show_function_1",
       ModuleName="UserCenter",
       Name="显示已关联接口",
       Order=0,
       ParentId=new Guid("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),
       Path="",
       Remark="显示已关联接口",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("858a52cd-fb89-ac4a-617a-262cc14e26d2"),
           ModuleName="UserCenter",
           ResourceId=new Guid("86a086a1-0770-4df4-ade3-433ff7226399"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="apartment",
       Id=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center",
       ModuleName="UserCenter",
       Name="用户中心",
       Order=15,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       Path="",
       Remark="用户中心",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="team",
       Id=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept",
       ModuleName="UserCenter",
       Name="部门管理",
       Order=3,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/user_center/dept",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80de8dfb-46cf-9a7a-8962-35f44bd6bb27"),
           ModuleName="UserCenter",
           ResourceId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_add",
       ModuleName="UserCenter",
       Name="添加部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2fa18090-8fb6-d304-2c65-b3ecb4cb019b"),
           ModuleName="UserCenter",
           ResourceId=new Guid("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),
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
       Id=new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_add_children",
       ModuleName="UserCenter",
       Name="添加子级部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2fa18090-8fb6-d304-2c65-b3ecb4cb019b"),
           ModuleName="UserCenter",
           ResourceId=new Guid("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),
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
       Id=new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_delete",
       ModuleName="UserCenter",
       Name="删除部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8f147e15-0bb0-4fba-68b0-31593a9d2607"),
           ModuleName="UserCenter",
           ResourceId=new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a271bdde-efd1-ce9a-119c-938f8d762a48"),
           ModuleName="UserCenter",
           ResourceId=new Guid("de62a886-64b2-4a40-b70a-47eb08f23202"),
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
       Id=new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d51378e3-1473-7dc5-7534-6b7cef9f994f"),
           ModuleName="UserCenter",
           ResourceId=new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d7b3f645-a48b-f8da-2a36-21a872d959e3"),
           ModuleName="UserCenter",
           ResourceId=new Guid("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),
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
       Id=new Guid("b63d694e-205f-44c0-8353-0c9507f44696"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_detail",
       ModuleName="UserCenter",
       Name="查看部门详情",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="查看部门详情",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ef51bcd9-6451-5b17-f8c8-7d2af3be637a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("b63d694e-205f-44c0-8353-0c9507f44696"),
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
       Id=new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_edit",
       ModuleName="UserCenter",
       Name="编辑部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("de749dff-4e45-0a4e-d9e4-53e06f84bdfb"),
           ModuleName="UserCenter",
           ResourceId=new Guid("316ecba5-5d89-44ae-908f-a54268723bd1"),
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
       Id=new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_dept_refresh",
       ModuleName="UserCenter",
       Name="刷新部门",
       Order=0,
       ParentId=new Guid("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80de8dfb-46cf-9a7a-8962-35f44bd6bb27"),
           ModuleName="UserCenter",
           ResourceId=new Guid("186bca5f-cc2c-427e-a58a-dbb81641a296"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="crown",
       Id=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position",
       ModuleName="UserCenter",
       Name="岗位管理",
       Order=5,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/user_center/position",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("134f9761-298f-04b8-7c64-073fe6ee7610"),
           ModuleName="UserCenter",
           ResourceId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_add",
       ModuleName="UserCenter",
       Name="添加岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8f1329d9-a219-da02-ab11-cea7123f861a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0fd84267-ee22-47c4-b41c-ce654eba29d9"),
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
       Id=new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_delete",
       ModuleName="UserCenter",
       Name="删除岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5b7af931-dcd5-a4d3-566a-8437b595a483"),
           ModuleName="UserCenter",
           ResourceId=new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e95e6e53-41be-52b2-05d5-204992abb386"),
           ModuleName="UserCenter",
           ResourceId=new Guid("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),
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
       Id=new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a1e6cbdb-e12d-eae2-1c02-528654c6ff5a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("df72b8ce-d5b9-6aed-6baf-c9944edac97d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),
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
       Id=new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_detail",
       ModuleName="UserCenter",
       Name="查看岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="查看岗位",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("17744d98-78b6-0d1d-bb55-b78abca83baf"),
           ModuleName="UserCenter",
           ResourceId=new Guid("ba89c7b7-552c-415c-b4be-085262dc76b0"),
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
       Id=new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_edit",
       ModuleName="UserCenter",
       Name="编辑岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7fa9d54f-d8ed-929c-804c-5e7a3745e704"),
           ModuleName="UserCenter",
           ResourceId=new Guid("25535592-81a1-42dd-8a55-509f2c852ff9"),
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
       Id=new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_lock",
       ModuleName="UserCenter",
       Name="锁定岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="锁定岗位",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d5b1eaf6-5f50-6223-0e6b-029b05299e9a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("94d2c383-03b6-475c-a744-637dd87a5fdc"),
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
       Id=new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_position_refresh",
       ModuleName="UserCenter",
       Name="刷新岗位",
       Order=0,
       ParentId=new Guid("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("134f9761-298f-04b8-7c64-073fe6ee7610"),
           ModuleName="UserCenter",
           ResourceId=new Guid("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="user-switch",
       Id=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role",
       ModuleName="UserCenter",
       Name="角色管理",
       Order=20,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/user_center/role",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("00ed0892-2b66-98a6-ff52-500c437164a6"),
           ModuleName="UserCenter",
           ResourceId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_add",
       ModuleName="UserCenter",
       Name="添加角色",
       Order=2,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
           new ResourceFunctionDto()
        {
           FunctionId=new Guid("4c426191-9298-4a23-0fe9-18830ebebf59"),
           ModuleName="UserCenter",
           ResourceId=new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c6742236-d221-1fc7-4269-e0c2b3d2c91d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("67501fd4-4fbf-48c2-b383-f3a2085268ed"),
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
       Id=new Guid("d982a072-4681-45d9-8489-7a14218adb04"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_delete",
       ModuleName="UserCenter",
       Name="删除角色",
       Order=1,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d4b49641-53c1-e6d1-33a4-35f2491338b0"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d982a072-4681-45d9-8489-7a14218adb04"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d8cda41d-6ff9-501c-f1e3-c11707308ce6"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d982a072-4681-45d9-8489-7a14218adb04"),
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
       Id=new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中角色",
       Order=0,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("021c9182-e45b-4d7e-2421-c55238fbfc24"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7f6ea5d3-8a9c-3776-0f68-09646d5cec1c"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a468499c-7115-44f1-ad38-2c5f696891d4"),
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
       Id=new Guid("2c1c895c-6434-4f14-91f2-144e48457101"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_detail",
       ModuleName="UserCenter",
       Name="查看角色详情",
       Order=0,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="查看角色详情",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2118b673-8bc6-bf53-67fb-f6ca528b64e2"),
           ModuleName="UserCenter",
           ResourceId=new Guid("2c1c895c-6434-4f14-91f2-144e48457101"),
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
       Id=new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_edit",
       ModuleName="UserCenter",
       Name="编辑角色",
       Order=4,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
           new ResourceFunctionDto()
        {
           FunctionId=new Guid("8d4a4537-a528-8b32-cbdc-1ecd2d685c69"),
           ModuleName="UserCenter",
           ResourceId=new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c6742236-d221-1fc7-4269-e0c2b3d2c91d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),
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
       Id=new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_lock",
       ModuleName="UserCenter",
       Name="锁定角色",
       Order=7,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("787b3581-641d-c458-d188-18ce86390349"),
           ModuleName="UserCenter",
           ResourceId=new Guid("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),
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
       Id=new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_refresh",
       ModuleName="UserCenter",
       Name="刷新角色",
       Order=3,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("00ed0892-2b66-98a6-ff52-500c437164a6"),
           ModuleName="UserCenter",
           ResourceId=new Guid("1efd01cf-42f2-45c7-95f2-84be55e65646"),
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
       Id=new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_resource_download_seed_data",
       ModuleName="UserCenter",
       Name="获取种子数据",
       Order=0,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("92fbdaf1-d388-50b6-8b40-27c8a71233c7"),
           ModuleName="UserCenter",
           ResourceId=new Guid("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),
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
       Id=new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_resource_select",
       ModuleName="UserCenter",
       Name="查看角色资源",
       Order=0,
       ParentId=new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),
       Path="",
       Remark="查看角色资源",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4f167e5c-48d6-c41e-5241-eed9f85fd830"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7b8b835d-691c-602e-f4f1-989c15127f49"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a2b68c70-173f-46fa-8442-e19219a9905b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e600186f-dfbe-40dc-bf5d-16a2a01ffc6a"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_set_is_super_administrator",
       ModuleName="UserCenter",
       Name="设置角色为超级管理员",
       Order=0,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Remark="分配该资源后，才能设置角色为超级管理员，请谨慎分配该资源。",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4c426191-9298-4a23-0fe9-18830ebebf59"),
           ModuleName="UserCenter",
           ResourceId=new Guid("e600186f-dfbe-40dc-bf5d-16a2a01ffc6a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8d4a4537-a528-8b32-cbdc-1ecd2d685c69"),
           ModuleName="UserCenter",
           ResourceId=new Guid("e600186f-dfbe-40dc-bf5d-16a2a01ffc6a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_set_resource",
       ModuleName="UserCenter",
       Name="角色分配资源",
       Order=5,
       ParentId=new Guid("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),
       Path="",
       Remark="",
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_role_set_resource_save",
       ModuleName="UserCenter",
       Name="保存角色资源",
       Order=0,
       ParentId=new Guid("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),
       Path="",
       Remark="保存角色资源",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3d585328-0562-ffd9-0827-73db2c7ea24c"),
           ModuleName="UserCenter",
           ResourceId=new Guid("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="deployment-unit",
       Id=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant",
       ModuleName="UserCenter",
       Name="租户管理",
       Order=0,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/user_center/tenant",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2653e355-3854-4775-1488-ac01cfbb7958"),
           ModuleName="UserCenter",
           ResourceId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    },new ResourceDto()
    {
       Hide=false,
       Id=new Guid("68c846ce-7ffd-402a-9ff0-353d7fd4af09"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_copy",
       ModuleName="UserCenter",
       Name="复制",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("08dd638f-f307-44fe-8f96-b0d9d7319e15"),
           ResourceId=new Guid("68c846ce-7ffd-402a-9ff0-353d7fd4af09"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d45effb9-67a8-4459-83ac-c3852c8b4f1f"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_add",
       ModuleName="UserCenter",
       Name="添加租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3d9786a5-689d-6170-e0b4-1ba621e999dd"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d45effb9-67a8-4459-83ac-c3852c8b4f1f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    },    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("fd4c8aef-d15c-4890-999e-582d51707cfb"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_tenant_administrator",
       ModuleName="UserCenter",
       Name="租户管理员",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       Remark="可以管理各数据的租户信息",
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d92268ec-6b51-4514-9487-52cb3fb0d850"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_delete",
       ModuleName="UserCenter",
       Name="删除租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5a43891a-db41-2b58-2c83-da9ccc658836"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d92268ec-6b51-4514-9487-52cb3fb0d850"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7d2e63d6-6334-325f-05b1-2369008cc85e"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d92268ec-6b51-4514-9487-52cb3fb0d850"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("efbcc18b-c193-42cc-b315-cde07f51b496"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("980b9f9b-5e3c-6a80-1e78-9e70b10d8182"),
           ModuleName="UserCenter",
           ResourceId=new Guid("efbcc18b-c193-42cc-b315-cde07f51b496"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fa4fe28e-1e3c-d62d-f254-9629579f6945"),
           ModuleName="UserCenter",
           ResourceId=new Guid("efbcc18b-c193-42cc-b315-cde07f51b496"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("07af05b1-6f3e-49fa-9959-463e246346df"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_detail",
       ModuleName="UserCenter",
       Name="查看租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("bdde521d-483d-3f44-54b2-66814c78eecc"),
           ModuleName="UserCenter",
           ResourceId=new Guid("07af05b1-6f3e-49fa-9959-463e246346df"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_edit",
       ModuleName="UserCenter",
       Name="编辑租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7dbc385c-9464-9ab2-aa6d-b9e026e13657"),
           ModuleName="UserCenter",
           ResourceId=new Guid("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_lock",
       ModuleName="UserCenter",
       Name="锁定租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a284dda1-c1f6-995c-43a4-27c58df22a75"),
           ModuleName="UserCenter",
           ResourceId=new Guid("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_refresh",
       ModuleName="UserCenter",
       Name="刷新租户",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2653e355-3854-4775-1488-ac01cfbb7958"),
           ModuleName="UserCenter",
           ResourceId=new Guid("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_set_resource",
       ModuleName="UserCenter",
       Name="绑定资源",
       Order=0,
       ParentId=new Guid("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("807029ec-be10-4faa-a332-bcb1021ff966"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_set_resource_save",
       ModuleName="UserCenter",
       Name="绑定资源-保存",
       Order=0,
       ParentId=new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("25689628-58ff-fcf1-5912-73af85a29b21"),
           ModuleName="UserCenter",
           ResourceId=new Guid("807029ec-be10-4faa-a332-bcb1021ff966"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("0898c23e-3c3c-4d7f-82ef-9255e11d9af8"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_tenant_set_resource_select",
       ModuleName="UserCenter",
       Name="查看已有资源",
       Order=0,
       ParentId=new Guid("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4f167e5c-48d6-c41e-5241-eed9f85fd830"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0898c23e-3c3c-4d7f-82ef-9255e11d9af8"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9de87489-5b1f-5079-3c67-678dd63e2f7d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0898c23e-3c3c-4d7f-82ef-9255e11d9af8"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="user",
       Id=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user",
       ModuleName="UserCenter",
       Name="用户管理",
       Order=10,
       ParentId=new Guid("bd892fb3-47b4-469e-ba14-7c0eb703e164"),
       Path="/user_center/user",
       Remark="用户管理",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2a651850-3713-debb-753d-63a79f3ce6be"),
           ModuleName="UserCenter",
           ResourceId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_add",
       ModuleName="UserCenter",
       Name="添加用户",
       Order=2,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{

            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7b2759c6-6b5c-939f-0f3d-4b558cf2276d"),
           ModuleName="UserCenter",
           ResourceId=new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80de8dfb-46cf-9a7a-8962-35f44bd6bb27"),
           ModuleName="UserCenter",
           ResourceId=new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8142d8e6-42de-6fc4-8316-9c341bc39625"),
           ModuleName="UserCenter",
           ResourceId=new Guid("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),
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
       Id=new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_delete",
       ModuleName="UserCenter",
       Name="删除用户",
       Order=1,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3a69e5f2-1936-f04c-b165-4a3d52792493"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f3414404-2e87-8998-2b6a-03d92e830e09"),
           ModuleName="UserCenter",
           ResourceId=new Guid("d5756ad0-6a8b-4462-907f-1c52a1e11369"),
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
       Id=new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_delete_selected",
       ModuleName="UserCenter",
       Name="删除选中用户",
       Order=0,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="删除选中",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5d50b0b6-c788-fd12-5784-3a5f8336df88"),
           ModuleName="UserCenter",
           ResourceId=new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9b86f692-bcdb-8546-a199-51ce0cc1f16a"),
           ModuleName="UserCenter",
           ResourceId=new Guid("476cf96a-0e18-4c30-a760-e8b9c615bb99"),
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
       Id=new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_detail",
       ModuleName="UserCenter",
       Name="查看用户",
       Order=0,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="查看用户",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("183255fe-4fca-d448-1b49-34ac085ecdbd"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80de8dfb-46cf-9a7a-8962-35f44bd6bb27"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8142d8e6-42de-6fc4-8316-9c341bc39625"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),
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
       Id=new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_edit",
       ModuleName="UserCenter",
       Name="编辑用户",
       Order=4,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80de8dfb-46cf-9a7a-8962-35f44bd6bb27"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8142d8e6-42de-6fc4-8316-9c341bc39625"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ad13c88b-b52b-9a98-d05e-011b20da71a6"),
           ModuleName="UserCenter",
           ResourceId=new Guid("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_export",
       ModuleName="UserCenter",
       Name="导出用户",
       Order=0,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Remark="导出用户",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03f51cd1-0e36-9034-63c5-ed348ad76ca6"),
           ModuleName="UserCenter",
           ResourceId=new Guid("a25da8f5-23d4-4118-b399-0a36f912a370"),
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
       Id=new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_list_edit_avatar",
       ModuleName="UserCenter",
       Name="编辑用户头像-列表中",
       Order=8,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="编辑用户头像-列表中",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03bde8d8-5b0c-bffa-7456-7c808676f815"),
           ModuleName="UserCenter",
           ResourceId=new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e9f6477c-10c8-0ae7-d714-0c8edf74f1cc"),
           ModuleName="UserCenter",
           ResourceId=new Guid("ea0fb035-1f06-4f61-9946-8df027a7462d"),
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
       Id=new Guid("87377abe-785d-426c-b052-f706a2c7173d"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_lock",
       ModuleName="UserCenter",
       Name="锁定用户",
       Order=7,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f4a0afef-de53-b6dd-9548-0a1091b36cb0"),
           ModuleName="UserCenter",
           ResourceId=new Guid("87377abe-785d-426c-b052-f706a2c7173d"),
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
       Id=new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_refresh",
       ModuleName="UserCenter",
       Name="刷新用户",
       Order=3,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2a651850-3713-debb-753d-63a79f3ce6be"),
           ModuleName="UserCenter",
           ResourceId=new Guid("e44bb45d-514c-4217-bfba-452c0bd38f28"),
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
       Id=new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_role_edit",
       ModuleName="UserCenter",
       Name="用户分配角色",
       Order=5,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{

            new ResourceFunctionDto()
        {
           FunctionId=new Guid("15e3d5c9-4052-950a-22be-777ab644eaba"),
           ModuleName="UserCenter",
           ResourceId=new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c0d498ce-1fa5-e1e6-ca0a-9943e2282f60"),
           ModuleName="UserCenter",
           ResourceId=new Guid("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"),
       IsDeleted=false,
       IsLocked=false,
       Key="user_center_user_role_edit_save",
       ModuleName="UserCenter",
       Name="保存用户角色",
       Order=0,
       ParentId=new Guid("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a8d8cb95-4443-a716-7203-8f1d9978e048"),
           ModuleName="UserCenter",
           ResourceId=new Guid("490bc05f-499e-4f4c-811d-fde4c10be2ed"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

});
            return result.ToArray();

        }

        /// <summary>
        /// 注册租户配置模板
        /// </summary>
        /// <returns></returns>
        public SystemTenantConfigTemplateDto[]? RegisterTenantConfigTemplate()
        {
            return [
                new SystemTenantConfigTemplateDto()
                {
                    ConfigKey="TenantMaxUserNumber",
                    DefaultConfigValue="20",
                    Description="限制该租户能创建用户的最大数量"
                },
                new SystemTenantConfigTemplateDto()
                {
                    ConfigKey="TenantMaxRoleNumber",
                    DefaultConfigValue="10",
                    Description="限制该租户能创建角色的最大数量"
                }
                ];
        }
    }
}
