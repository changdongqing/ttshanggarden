// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Services;
using Gardener.Core.Audit;
using Gardener.Core.Module;

namespace Gardener.Core.Api.Impl.Audit
{
    /// <summary>
    /// 模块
    /// </summary>
    public class AuditServerModule : AuditModule, IServerModule
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
                return [typeof(AuditEntityService), typeof(AuditFunctionService)];
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
       Icon="audit",
       Id=new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit",
       ModuleName="Audit",
       Name="审计管理",
       Order=60,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="",
       Remark="审计管理",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity",
       ModuleName="Audit",
       Name="数据审计",
       Order=2,
       ParentId=new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"),
       Path="/system_manager/audit-entity",
       Remark="数据审计",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity_delete",
       ModuleName="Audit",
       Name="删除数据审计",
       Order=3,
       ParentId=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       Path="",
       Remark="删除数据审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2380e77e-5061-e0ed-5682-023ebe5d99c7"),
           ModuleName="Audit",
           ResourceId=new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d047fb8c-22c6-84c1-bc68-5c9c8ab889ce"),
           ModuleName="Audit",
           ResourceId=new Guid("286dc779-f58d-439a-bb9b-1333ff2b111b"),
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
       Id=new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity_delete_selected",
       ModuleName="Audit",
       Name="删除选中数据审计",
       Order=2,
       ParentId=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       Path="",
       Remark="删除选中数据审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("46b710ce-42bb-dc49-8b5c-66e1e0c25519"),
           ModuleName="Audit",
           ResourceId=new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("56451ed0-eb26-40d8-e06f-7f2f58a7e11a"),
           ModuleName="Audit",
           ResourceId=new Guid("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),
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
       Id=new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity_detail",
       ModuleName="Audit",
       Name="查询数据审计详情",
       Order=4,
       ParentId=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       Path="",
       Remark="查询数据审计详情",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f56e1e8e-bfad-13d9-33e1-6bf62d8bd052"),
           ModuleName="Audit",
           ResourceId=new Guid("3f8d700a-bc26-4d5c-9622-d98bf9359159"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity_export",
       ModuleName="Audit",
       Name="导出数据审计",
       Order=0,
       ParentId=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       Remark="导出数据审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("26d22c8f-3485-74de-913b-f4e69563684a"),
           ModuleName="Audit",
           ResourceId=new Guid("4f259695-23ea-4453-a4f1-2b055d135c37"),
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
       Id=new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_entity_refresh",
       ModuleName="Audit",
       Name="刷新数据审计",
       Order=1,
       ParentId=new Guid("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),
       Path="",
       Remark="刷新数据审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("550340dc-9660-a322-c589-1dcb740bc5f7"),
           ModuleName="Audit",
           ResourceId=new Guid("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),
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
       Id=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function",
       ModuleName="Audit",
       Name="接口审计",
       Order=1,
       ParentId=new Guid("2dd1a78c-f725-461b-8bc6-66112a7e156c"),
       Path="/system_manager/audit-function",
       Remark="接口审计",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_delete",
       ModuleName="Audit",
       Name="删除接口审计",
       Order=3,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       Path="",
       Remark="删除接口审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ae740c88-cab3-77d4-dac4-52e943692919"),
           ModuleName="Audit",
           ResourceId=new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d76d9e9f-d3d0-f735-b4b6-6aec049cf0c7"),
           ModuleName="Audit",
           ResourceId=new Guid("1c377037-13b4-4ef2-8010-d914a40fdbb3"),
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
       Id=new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_delete_selected",
       ModuleName="Audit",
       Name="删除选中接口审计",
       Order=2,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c95c184e-201e-ce40-fb4a-e91e445f914a"),
           ModuleName="Audit",
           ResourceId=new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ebf0eecb-a36d-0b7f-1856-a40ba423f76d"),
           ModuleName="Audit",
           ResourceId=new Guid("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),
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
       Id=new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_detail",
       ModuleName="Audit",
       Name="接口审计数据变更详情",
       Order=0,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       Path="",
       Remark="接口审计数据变更详情",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9d6726d9-d570-e578-5940-3dbaa41fcf09"),
           ModuleName="Audit",
           ResourceId=new Guid("24ace337-41fe-429d-b32e-d9f88bd97aaa"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_export",
       ModuleName="Audit",
       Name="导出接口审计",
       Order=0,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       Remark="导出接口审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cb75f702-8536-4ff8-ef19-976137eceb66"),
           ModuleName="Audit",
           ResourceId=new Guid("2ac78309-1719-4ea5-ac0f-6974a86f168c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("0dc989c3-d60a-4ac3-89be-87f485ca820d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_parameters",
       ModuleName="Audit",
       Name="查看接口审计参数",
       Order=0,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_audit_function_refresh",
       ModuleName="Audit",
       Name="刷新接口审计",
       Order=1,
       ParentId=new Guid("b8224935-fae6-4bbe-ad91-1d8969baabe8"),
       Path="",
       Remark="刷新接口审计",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("92246fa0-d23c-03fe-603b-da4d282baef4"),
           ModuleName="Audit",
           ResourceId=new Guid("cc23917b-930a-4e34-9717-be71b9fd2dd5"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
