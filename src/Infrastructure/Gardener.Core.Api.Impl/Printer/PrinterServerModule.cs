// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Printer.Services;
using Gardener.Core.Dict.Dtos;
using Gardener.Core.Module;
using Gardener.Core.Printer;

namespace Gardener.Core.Api.Impl.Printer
{
    /// <summary>
    /// 
    /// </summary>
    public class PrinterServerModule : PrinterModule, IServerModule
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
                    typeof(PrintService),
                    typeof(PrintTemplateService)
                    ];
            }
        }

        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        public CodeTypeDto[]? RegisterDic()
        {
            return [
                 new CodeTypeDto()
    {
       CodeTypeName="打印模板类型",
       CodeTypeValue="print_template_type",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-09-24 15:19:01"),
       CreateIdentityType=IdentityType.User,
       Id=5,
       IsDeleted=false,
       IsLocked=false,
       Codes=[
               new CodeDto()
    {
       CodeName="打印机测试内容",
       CodeTypeId=5,
       CodeValue="test_template",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-09-24 15:19:49"),
       CreateIdentityType=IdentityType.User,
       Id=23,
       IsDeleted=false,
       IsLocked=false,
       Order=0,
    }
,
    new CodeDto()
    {
       CodeName="过磅单",
       CodeTypeId=5,
       CodeValue="wbg_print_detail",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-09-24 15:20:22"),
       CreateIdentityType=IdentityType.User,
       Id=24,
       IsDeleted=false,
       IsLocked=false,
       Order=1,
    }
           ]
    }

                ];
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
       Icon="printer",
       Id=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template",
       ModuleName="Printer",
       Name="打印模板",
       Order=120,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_manager/print_template",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("fa8779f1-c373-462d-9597-cb2614a02914"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_add",
       ModuleName="Printer",
       Name="添加",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4363ac7b-bf29-7a41-9a2c-538fcafc3f96"),
           ModuleName="Printer",
           ResourceId=new Guid("fa8779f1-c373-462d-9597-cb2614a02914"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4ca581ae-7525-440d-ab20-181273f7d4a7"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_delete",
       ModuleName="Printer",
       Name="删除",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("69b2328c-d142-e466-4da7-f18d60bf18c9"),
           ModuleName="Printer",
           ResourceId=new Guid("4ca581ae-7525-440d-ab20-181273f7d4a7"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9f37a4fa-00d6-37f4-9358-8a115af9f050"),
           ModuleName="Printer",
           ResourceId=new Guid("4ca581ae-7525-440d-ab20-181273f7d4a7"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e9bd08ec-f63f-4558-b11c-a5c21892cd1a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_delete_selected",
       ModuleName="Printer",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d5913e7a-87d5-6fbc-2922-ba047379b68f"),
           ModuleName="Printer",
           ResourceId=new Guid("e9bd08ec-f63f-4558-b11c-a5c21892cd1a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fbaf84f1-9189-ff4e-040e-0dbc927c28ca"),
           ModuleName="Printer",
           ResourceId=new Guid("e9bd08ec-f63f-4558-b11c-a5c21892cd1a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7b160e37-5c3f-4d78-bd1e-2b13362f42d1"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_detail",
       ModuleName="Printer",
       Name="详情",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8e7f976d-fc81-0946-7636-171bf21b92f5"),
           ModuleName="Printer",
           ResourceId=new Guid("7b160e37-5c3f-4d78-bd1e-2b13362f42d1"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("19057c05-eb92-4200-8fd4-bbbb2944c393"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_lock",
       ModuleName="Printer",
       Name="锁定",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3ec413dc-ccb3-6bcd-6476-e018d18b626d"),
           ModuleName="Printer",
           ResourceId=new Guid("19057c05-eb92-4200-8fd4-bbbb2944c393"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ba48deec-dbba-4709-94d5-9a7ec44180f9"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_refresh",
       ModuleName="Printer",
       Name="刷新",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f970dc46-24af-26ae-5e14-2ece15cec1fc"),
           ModuleName="Printer",
           ResourceId=new Guid("ba48deec-dbba-4709-94d5-9a7ec44180f9"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("92b85afb-24c1-4778-95f9-07f5e1b8a44f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_search",
       ModuleName="Printer",
       Name="搜索",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f970dc46-24af-26ae-5e14-2ece15cec1fc"),
           ModuleName="Printer",
           ResourceId=new Guid("92b85afb-24c1-4778-95f9-07f5e1b8a44f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("6c4e6dfc-f130-427f-b65d-ee538a0cdd97"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_print_template_update",
       ModuleName="Printer",
       Name="更新",
       Order=0,
       ParentId=new Guid("71c3eff5-9f93-428c-891a-078e63542259"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8e7f976d-fc81-0946-7636-171bf21b92f5"),
           ModuleName="Printer",
           ResourceId=new Guid("6c4e6dfc-f130-427f-b65d-ee538a0cdd97"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d954c9dd-6172-ddd5-f1c0-7c804d1b16b5"),
           ModuleName="Printer",
           ResourceId=new Guid("6c4e6dfc-f130-427f-b65d-ee538a0cdd97"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
