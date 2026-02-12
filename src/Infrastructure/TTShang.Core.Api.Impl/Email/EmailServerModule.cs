// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Email.Services;
using TTShang.Core.Email;
using TTShang.Core.Module;

namespace TTShang.Core.Api.Impl.Email
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EmailServerModule : EmailModule, IServerModule
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
                return [typeof(EmailServerConfigService), typeof(EmailService), typeof(EmailTemplateService)];
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
       Icon="setting",
       Id=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config",
       ModuleName="Email",
       Name="邮件服务器",
       Order=10,
       ParentId=new Guid("6dc2b297-7110-462a-b402-9e9736abf292"),
       Path="/system_manager/email_server_config",
       Remark="邮件服务器配置",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_add",
       ModuleName="Email",
       Name="添加邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="添加邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b0458d7b-f0c2-6744-c40f-8e1420475a42"),
           ModuleName="Email",
           ResourceId=new Guid("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),
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
       Id=new Guid("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_delete",
       ModuleName="Email",
       Name="删除邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="删除邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7ebaecdb-d048-d983-417d-ed5cbdebf86f"),
           ModuleName="Email",
           ResourceId=new Guid("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a6756fbb-9259-1beb-fb92-5dcaee56f8a4"),
           ModuleName="Email",
           ResourceId=new Guid("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),
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
       Id=new Guid("1f8605fb-70b3-4929-89eb-4cda69cc305b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_delete_selected",
       ModuleName="Email",
       Name="删除选中邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="删除选中邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1d8b0ad8-82e0-9296-a3bc-d5e834fcdea7"),
           ModuleName="Email",
           ResourceId=new Guid("1f8605fb-70b3-4929-89eb-4cda69cc305b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d0f6d823-3ee1-1228-4796-66a7fc16a052"),
           ModuleName="Email",
           ResourceId=new Guid("1f8605fb-70b3-4929-89eb-4cda69cc305b"),
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
       Id=new Guid("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_detail",
       ModuleName="Email",
       Name="查看邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="查看邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f21ead61-ae52-927d-5bc1-db2a711db29e"),
           ModuleName="Email",
           ResourceId=new Guid("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),
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
       Id=new Guid("106a3a28-3143-4369-9215-cb223d1b0e45"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_edit",
       ModuleName="Email",
       Name="编辑邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="编辑邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0e5331f3-fb76-5b62-5254-ba78b40849c7"),
           ModuleName="Email",
           ResourceId=new Guid("106a3a28-3143-4369-9215-cb223d1b0e45"),
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
       Id=new Guid("02337e03-c44f-4029-bbb2-0cc5adf84c29"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_lock",
       ModuleName="Email",
       Name="锁定邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="锁定邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("71151012-95f8-9074-5b73-a8162d361b16"),
           ModuleName="Email",
           ResourceId=new Guid("02337e03-c44f-4029-bbb2-0cc5adf84c29"),
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
       Id=new Guid("d697fda5-28fa-46c3-ba88-a98dd510e09d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_refresh",
       ModuleName="Email",
       Name="刷新邮件服务器配置",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="刷新邮件服务器配置",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3b0c8509-3bf3-b469-ebfc-a8007edf783d"),
           ModuleName="Email",
           ResourceId=new Guid("d697fda5-28fa-46c3-ba88-a98dd510e09d"),
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
       Id=new Guid("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_server_config_send",
       ModuleName="Email",
       Name="发送测试邮件",
       Order=0,
       ParentId=new Guid("99c74c8b-e343-43bc-86e3-bca825b6a270"),
       Path="",
       Remark="发送测试邮件",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2ffd7851-b418-3e82-e6be-053286ff00f9"),
           ModuleName="Email",
           ResourceId=new Guid("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4afb1f15-a8f3-eaf6-7a34-f9b73041bd85"),
           ModuleName="Email",
           ResourceId=new Guid("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="copy",
       Id=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_temaplate",
       ModuleName="Email",
       Name="邮件模板",
       Order=20,
       ParentId=new Guid("6dc2b297-7110-462a-b402-9e9736abf292"),
       Path="/system_manager/email_temaplate",
       Remark="邮件模板",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("083fffc4-2600-49bb-87e6-1a92133499ec"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_add",
       ModuleName="Email",
       Name="添加邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="添加邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a2491718-1ff4-ee57-d55d-7d23cc4e5c8e"),
           ModuleName="Email",
           ResourceId=new Guid("083fffc4-2600-49bb-87e6-1a92133499ec"),
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
       Id=new Guid("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_delete",
       ModuleName="Email",
       Name="删除邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="删除邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0b27edbf-e69c-a913-2b56-18c038937f62"),
           ModuleName="Email",
           ResourceId=new Guid("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d429c24a-d530-ccc8-4aec-d84755e41bbd"),
           ModuleName="Email",
           ResourceId=new Guid("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),
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
       Id=new Guid("145ec764-6a72-4c4f-85d3-7ad889193970"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_delete_selected",
       ModuleName="Email",
       Name="删除选中邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="删除选中邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0b27edbf-e69c-a913-2b56-18c038937f62"),
           ModuleName="Email",
           ResourceId=new Guid("145ec764-6a72-4c4f-85d3-7ad889193970"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1267cc4f-65e0-1f8f-963b-2c703aa7f3d0"),
           ModuleName="Email",
           ResourceId=new Guid("145ec764-6a72-4c4f-85d3-7ad889193970"),
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
       Id=new Guid("7aad6dba-3f13-4982-adfa-525fa94485dd"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_detail",
       ModuleName="Email",
       Name="查看邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="查看邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("96aace7e-e64d-57c2-0890-73986f59b00d"),
           ModuleName="Email",
           ResourceId=new Guid("7aad6dba-3f13-4982-adfa-525fa94485dd"),
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
       Id=new Guid("08baa5af-4718-4158-9276-1ad1068b9159"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_edit",
       ModuleName="Email",
       Name="编辑邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="编辑邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("649c91fd-4a62-d929-7fae-dfa2431f6652"),
           ModuleName="Email",
           ResourceId=new Guid("08baa5af-4718-4158-9276-1ad1068b9159"),
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
       Id=new Guid("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_lock",
       ModuleName="Email",
       Name="锁定邮件模板",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="锁定邮件模板",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("45ccd844-9c78-372f-c0d5-90939d665c4e"),
           ModuleName="Email",
           ResourceId=new Guid("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),
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
       Id=new Guid("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_refresh",
       ModuleName="Email",
       Name="刷新邮件模板列表",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="刷新邮件模板列表",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("dc2d3197-ac8f-618c-3dbe-d402d65a259b"),
           ModuleName="Email",
           ResourceId=new Guid("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),
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
       Id=new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_template_send",
       ModuleName="Email",
       Name="发送测试邮件",
       Order=0,
       ParentId=new Guid("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),
       Path="",
       Remark="发送测试邮件",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("46b4c9d9-b7af-36d3-c303-1a9b8509e12f"),
           ModuleName="Email",
           ResourceId=new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4afb1f15-a8f3-eaf6-7a34-f9b73041bd85"),
           ModuleName="Email",
           ResourceId=new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("96aace7e-e64d-57c2-0890-73986f59b00d"),
           ModuleName="Email",
           ResourceId=new Guid("af9b9a49-0094-4e1c-97dc-d0580525244f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="mail",
       Id=new Guid("6dc2b297-7110-462a-b402-9e9736abf292"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_email_tool",
       ModuleName="Email",
       Name="邮件工具",
       Order=80,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="",
       Remark="邮件工具",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }

};
        }
    }
}
