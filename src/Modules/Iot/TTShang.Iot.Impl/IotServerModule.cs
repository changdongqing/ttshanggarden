// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;
using TTShang.Core.Module;
using TTShang.Core.SystemAsset.Dtos;

namespace TTShang.Iot.Impl
{
    /// <summary>
    /// 模块
    /// </summary>
    public class IotServerModule : IotModule, IServerModule
    {

        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        public CodeTypeDto[]? RegisterDic()
        {
            return new CodeTypeDto[]{

             new CodeTypeDto()
    {
       CodeTypeName="物联网设备类型",
       CodeTypeValue="iot-product-type",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-06-07 14:45:36"),
       CreateIdentityType=IdentityType.User,
       Id=2,
       IsDeleted=false,
       IsLocked=false,
       Codes=new[]{
    new CodeDto()
    {
       CodeName="传感器",
       CodeTypeId=2,
       CodeValue="transducer",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-06-07 14:46:10"),
       CreateIdentityType=IdentityType.User,
       Id=11,
       IsDeleted=false,
       IsLocked=false,
       Order=1,
    }
,
    new CodeDto()
    {
       CodeName="发声器",
       CodeTypeId=2,
       CodeValue="sounder",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-06-07 14:49:02"),
       CreateIdentityType=IdentityType.User,
       Id=13,
       IsDeleted=false,
       IsLocked=false,
       Order=2,
    }
,
    new CodeDto()
    {
       CodeName="显示器",
       CodeTypeId=2,
       CodeValue="displayer",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-06-07 14:48:24"),
       CreateIdentityType=IdentityType.User,
       Id=12,
       IsDeleted=false,
       IsLocked=false,
       Order=3,
    }
,
    new CodeDto()
    {
       CodeName="混合设备",
       CodeTypeId=2,
       CodeValue="hybrid-equipment",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-06-07 14:49:40"),
       CreateIdentityType=IdentityType.User,
       Id=14,
       IsDeleted=false,
       IsLocked=false,
       Order=4,
    },new CodeDto()
    {
       CodeName="地磅T100",
       CodeTypeId=2,
       CodeValue="weighbridge-t100",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-09-06 13:14:33"),
       CreateIdentityType=IdentityType.User,
       Id=21,
       IsDeleted=false,
       IsLocked=false,
       Order=100,
    }
,
    new CodeDto()
    {
       CodeName="地磅T200",
       CodeTypeId=2,
       CodeValue="weighbridge-t200",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-09-06 13:15:05"),
       CreateIdentityType=IdentityType.User,
       Id=22,
       IsDeleted=false,
       IsLocked=false,
       Order=200,
    }

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
            return new[]{
                    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("14328f55-9ce6-4714-ade3-e3fbb3718265"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_generate_connect_config",
       ModuleName="Iot",
       Name="生成连接配置",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    },
    new ResourceDto()
    {
       Hide=false,
       Icon="iconfont icon-iot",
       Id=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot",
       ModuleName="Iot",
       Name="物联网",
       Order=300,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device",
       ModuleName="Iot",
       Name="设备",
       Order=20,
       ParentId=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       Path="/iot/device_manager",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection",
       ModuleName="Iot",
       Name="设备连接",
       Order=30,
       ParentId=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       Path="/iot/device_connection",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c1e7b69b-567f-4efb-84b3-9e958c318b2a"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_delete",
       ModuleName="Iot",
       Name="删除",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("180a3a8e-ecfb-bfc9-aedf-31aa04c6e35a"),
           ModuleName="Iot",
           ResourceId=new Guid("c1e7b69b-567f-4efb-84b3-9e958c318b2a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("bcb4e580-03ed-1216-90b6-df2ae7f59a90"),
           ModuleName="Iot",
           ResourceId=new Guid("c1e7b69b-567f-4efb-84b3-9e958c318b2a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("fa538b34-1840-4a07-ada0-2618fdc1880c"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_delete_selected",
       ModuleName="Iot",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("65dde651-65e6-4236-fa61-1ac7615cfd09"),
           ModuleName="Iot",
           ResourceId=new Guid("fa538b34-1840-4a07-ada0-2618fdc1880c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a20dae32-c4f3-90b9-c6ea-050c2a087984"),
           ModuleName="Iot",
           ResourceId=new Guid("fa538b34-1840-4a07-ada0-2618fdc1880c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9f004145-612b-404a-9365-fd2094b9879d"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_detail",
       ModuleName="Iot",
       Name="详情",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9124b77e-dd25-e36b-6e9a-ce74a7d24d43"),
           ModuleName="Iot",
           ResourceId=new Guid("9f004145-612b-404a-9365-fd2094b9879d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1bb7b3ee-9d55-4182-8d13-1fd86f2aeadd"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_refresh",
       ModuleName="Iot",
       Name="刷新",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8603d4ce-4f7f-e3f5-4d9a-1826fedf8f0a"),
           ModuleName="Iot",
           ResourceId=new Guid("1bb7b3ee-9d55-4182-8d13-1fd86f2aeadd"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("017a9efd-59cb-48ae-85eb-d21d50855f6f"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_search",
       ModuleName="Iot",
       Name="搜索",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8603d4ce-4f7f-e3f5-4d9a-1826fedf8f0a"),
           ModuleName="Iot",
           ResourceId=new Guid("017a9efd-59cb-48ae-85eb-d21d50855f6f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7d6ca75c-befd-433f-a19d-59d4fe698bbb"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_connection_show_logs",
       ModuleName="Iot",
       Name="查询日志",
       Order=0,
       ParentId=new Guid("72567927-ff8e-4369-a876-8b102ac02768"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group",
       ModuleName="Iot",
       Name="设备组",
       Order=10,
       ParentId=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       Path="/iot/device_group",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bc993478-e731-4b62-a3bc-d1172f76a24c"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_add",
       ModuleName="Iot",
       Name="添加",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e837e9f1-90a0-1c29-b97c-4e95120c0d96"),
           ModuleName="Iot",
           ResourceId=new Guid("bc993478-e731-4b62-a3bc-d1172f76a24c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b554e3b2-ccf7-4db4-9643-686a05ac157c"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_add_children",
       ModuleName="Iot",
       Name="添加子集",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e837e9f1-90a0-1c29-b97c-4e95120c0d96"),
           ModuleName="Iot",
           ResourceId=new Guid("b554e3b2-ccf7-4db4-9643-686a05ac157c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("34618609-b095-4aa9-a640-7ce58b291ac6"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_delete",
       ModuleName="Iot",
       Name="删除",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6e01453b-5ad4-444a-0a96-35d8b7a048af"),
           ModuleName="Iot",
           ResourceId=new Guid("34618609-b095-4aa9-a640-7ce58b291ac6"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e56e92a9-6e6e-11cd-1b0a-587785549779"),
           ModuleName="Iot",
           ResourceId=new Guid("34618609-b095-4aa9-a640-7ce58b291ac6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5b9d4434-d895-4324-a9d2-9fd24b00fe6b"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_delete_selected",
       ModuleName="Iot",
       Name="删除选中行",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("358a0611-0dbc-6e17-0503-372a7aabc929"),
           ModuleName="Iot",
           ResourceId=new Guid("5b9d4434-d895-4324-a9d2-9fd24b00fe6b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ac756f96-7bb1-86e2-0ba2-667314796c7b"),
           ModuleName="Iot",
           ResourceId=new Guid("5b9d4434-d895-4324-a9d2-9fd24b00fe6b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ae857242-f17d-4694-8e48-cad8e3105a90"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_detail",
       ModuleName="Iot",
       Name="详情",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("21e32609-ae90-361e-3995-9bf5bbd02c4d"),
           ModuleName="Iot",
           ResourceId=new Guid("ae857242-f17d-4694-8e48-cad8e3105a90"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("91c3db77-2569-462c-b299-a9d4fce3ad78"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_edit",
       ModuleName="Iot",
       Name="编辑",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1b21e2bd-42b6-68d5-6c90-1d1fbef2e687"),
           ModuleName="Iot",
           ResourceId=new Guid("91c3db77-2569-462c-b299-a9d4fce3ad78"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c28f1e96-4189-4e56-8d5b-81aa4fd701f6"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_group_refresh",
       ModuleName="Iot",
       Name="刷新列表",
       Order=0,
       ParentId=new Guid("18f5aa6c-f745-4319-94e7-d581e4386051"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2995d281-2060-04ee-c24e-bb7f2c4083dc"),
           ModuleName="Iot",
           ResourceId=new Guid("c28f1e96-4189-4e56-8d5b-81aa4fd701f6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1d19973f-1cef-4fbc-a616-6d36938877a3"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_add",
       ModuleName="Iot",
       Name="添加",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("06cdd813-de55-b3f9-2c40-10ba4f14d930"),
           ModuleName="Iot",
           ResourceId=new Guid("1d19973f-1cef-4fbc-a616-6d36938877a3"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a28cc707-6dbe-ea9f-f7ff-3c6c61238122"),
           ModuleName="Iot",
           ResourceId=new Guid("1d19973f-1cef-4fbc-a616-6d36938877a3"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a57d49f7-0688-94b8-26a9-7ea6548b7e9e"),
           ModuleName="Iot",
           ResourceId=new Guid("1d19973f-1cef-4fbc-a616-6d36938877a3"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e514e60f-c6b3-424b-49bf-532c9206518d"),
           ModuleName="Iot",
           ResourceId=new Guid("1d19973f-1cef-4fbc-a616-6d36938877a3"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("50b06a33-b562-4519-93a7-fd266d8a64ba"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_bind",
       ModuleName="Iot",
       Name="绑定",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03d633c8-be42-cb92-ab67-27b10bc79b9b"),
           ModuleName="Iot",
           ResourceId=new Guid("50b06a33-b562-4519-93a7-fd266d8a64ba"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("99d6de20-208a-43fa-aef7-d1e9426e3309"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_delete",
       ModuleName="Iot",
       Name="删除",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("06c36e53-9162-674a-32b0-beb3b3f7184d"),
           ModuleName="Iot",
           ResourceId=new Guid("99d6de20-208a-43fa-aef7-d1e9426e3309"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("bb55c125-5de0-960a-7e57-26fb8eb7ce17"),
           ModuleName="Iot",
           ResourceId=new Guid("99d6de20-208a-43fa-aef7-d1e9426e3309"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ed7dd1fe-a0c6-4e2b-90d2-c6994590445f"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_delete_selected",
       ModuleName="Iot",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("23682b8b-a45c-d53e-3af0-4e43d1239268"),
           ModuleName="Iot",
           ResourceId=new Guid("ed7dd1fe-a0c6-4e2b-90d2-c6994590445f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("85bf7235-3e20-e815-9a57-175dd2f3edb0"),
           ModuleName="Iot",
           ResourceId=new Guid("ed7dd1fe-a0c6-4e2b-90d2-c6994590445f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("6cbed665-65fd-488e-bff7-1069296f79e0"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_detail",
       ModuleName="Iot",
       Name="查看详情",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3be6f57b-8c5d-bae2-7207-758adaea85a2"),
           ModuleName="Iot",
           ResourceId=new Guid("6cbed665-65fd-488e-bff7-1069296f79e0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9124b77e-dd25-e36b-6e9a-ce74a7d24d43"),
           ModuleName="Iot",
           ResourceId=new Guid("6cbed665-65fd-488e-bff7-1069296f79e0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a28cc707-6dbe-ea9f-f7ff-3c6c61238122"),
           ModuleName="Iot",
           ResourceId=new Guid("6cbed665-65fd-488e-bff7-1069296f79e0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e514e60f-c6b3-424b-49bf-532c9206518d"),
           ModuleName="Iot",
           ResourceId=new Guid("6cbed665-65fd-488e-bff7-1069296f79e0"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a4435928-2f20-46dd-b393-445cfa7c9e19"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_edit",
       ModuleName="Iot",
       Name="编辑",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a28cc707-6dbe-ea9f-f7ff-3c6c61238122"),
           ModuleName="Iot",
           ResourceId=new Guid("a4435928-2f20-46dd-b393-445cfa7c9e19"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a57d49f7-0688-94b8-26a9-7ea6548b7e9e"),
           ModuleName="Iot",
           ResourceId=new Guid("a4435928-2f20-46dd-b393-445cfa7c9e19"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d5a1223b-5a92-2ff1-fe00-30b8561f4d5c"),
           ModuleName="Iot",
           ResourceId=new Guid("a4435928-2f20-46dd-b393-445cfa7c9e19"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e514e60f-c6b3-424b-49bf-532c9206518d"),
           ModuleName="Iot",
           ResourceId=new Guid("a4435928-2f20-46dd-b393-445cfa7c9e19"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("0c915133-df03-43f5-90c9-42e5e712b251"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_refresh",
       ModuleName="Iot",
       Name="刷新",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5c756f11-d959-2212-1875-dd4ca9042b90"),
           ModuleName="Iot",
           ResourceId=new Guid("0c915133-df03-43f5-90c9-42e5e712b251"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a28cc707-6dbe-ea9f-f7ff-3c6c61238122"),
           ModuleName="Iot",
           ResourceId=new Guid("0c915133-df03-43f5-90c9-42e5e712b251"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e514e60f-c6b3-424b-49bf-532c9206518d"),
           ModuleName="Iot",
           ResourceId=new Guid("0c915133-df03-43f5-90c9-42e5e712b251"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e6125b8b-bb9a-4355-8772-ccf95cdfba9d"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_show_connections",
       ModuleName="Iot",
       Name="查询连接",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("166b00ab-88b7-4acd-ad8b-381bea2203fc"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_manager_show_logs",
       ModuleName="Iot",
       Name="查询日志",
       Order=0,
       ParentId=new Guid("2e2a30bc-1e04-423c-b75e-204da8d21034"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("f47d5148-7511-46e5-8c3f-dd232ff39244"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_system_delete_selected",
       ModuleName="Iot",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("628409bb-82cb-41d1-a0ad-1fd5a2b03806"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9b87211b-b7c6-b7c2-d19e-116d062553f8"),
           ModuleName="Iot",
           ResourceId=new Guid("f47d5148-7511-46e5-8c3f-dd232ff39244"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("afb108af-e798-af9f-dbe9-9e1f11d3d4e6"),
           ModuleName="Iot",
           ResourceId=new Guid("f47d5148-7511-46e5-8c3f-dd232ff39244"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("628409bb-82cb-41d1-a0ad-1fd5a2b03806"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_system_log",
       ModuleName="Iot",
       Name="设备日志",
       Order=40,
       ParentId=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       Path="/iot/device_system_log",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c7e285a7-e089-4e33-bea3-cc9b464402ac"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_system_log_refresh",
       ModuleName="Iot",
       Name="刷新",
       Order=0,
       ParentId=new Guid("628409bb-82cb-41d1-a0ad-1fd5a2b03806"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("57addf88-0d96-6174-4015-fe0c56078caf"),
           ModuleName="Iot",
           ResourceId=new Guid("c7e285a7-e089-4e33-bea3-cc9b464402ac"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4c00f794-1225-4971-aad0-826f6cc59ac4"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_device_system_log_search",
       ModuleName="Iot",
       Name="搜索",
       Order=0,
       ParentId=new Guid("628409bb-82cb-41d1-a0ad-1fd5a2b03806"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("57addf88-0d96-6174-4015-fe0c56078caf"),
           ModuleName="Iot",
           ResourceId=new Guid("4c00f794-1225-4971-aad0-826f6cc59ac4"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product",
       ModuleName="Iot",
       Name="产品",
       Order=0,
       ParentId=new Guid("9489942e-f4ea-48f3-8b2e-2f128fa3b40f"),
       Path="/iot/product",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e450dc41-8d56-47e8-b101-45d97fdaf037"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_add",
       ModuleName="Iot",
       Name="添加",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03bde8d8-5b0c-bffa-7456-7c808676f815"),
           ModuleName="Iot",
           ResourceId=new Guid("e450dc41-8d56-47e8-b101-45d97fdaf037"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b4d0f205-919e-1a45-bfdf-68d7a751781d"),
           ModuleName="Iot",
           ResourceId=new Guid("e450dc41-8d56-47e8-b101-45d97fdaf037"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fef49872-7197-b395-0d66-6b6fdde42d6c"),
           ModuleName="Iot",
           ResourceId=new Guid("e450dc41-8d56-47e8-b101-45d97fdaf037"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1044b531-9e31-4047-88fe-b13d6ba42121"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_delete",
       ModuleName="Iot",
       Name="删除",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0dc64eac-b764-5928-5cd3-7749c4f9b614"),
           ModuleName="Iot",
           ResourceId=new Guid("1044b531-9e31-4047-88fe-b13d6ba42121"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5facee7b-fdbb-6658-91a6-2018077cba56"),
           ModuleName="Iot",
           ResourceId=new Guid("1044b531-9e31-4047-88fe-b13d6ba42121"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("19f94242-4d5f-4902-852c-c69cb375bd44"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_delete_selected",
       ModuleName="Iot",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1502e768-7d85-ce9a-67a3-0148dcf71d4b"),
           ModuleName="Iot",
           ResourceId=new Guid("19f94242-4d5f-4902-852c-c69cb375bd44"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c5431731-e784-7acd-5886-ed83037a127f"),
           ModuleName="Iot",
           ResourceId=new Guid("19f94242-4d5f-4902-852c-c69cb375bd44"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8d2af2cc-6ee0-411a-837c-55fb308392f6"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_detail",
       ModuleName="Iot",
       Name="详情",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("826c8c74-c433-29d3-054b-f0c4c1d6da39"),
           ModuleName="Iot",
           ResourceId=new Guid("8d2af2cc-6ee0-411a-837c-55fb308392f6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("21e2e09d-e941-4a19-b01d-decd8783612b"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_lock",
       ModuleName="Iot",
       Name="锁定",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a6115e53-f6a8-3e4e-3247-6d9f9f296795"),
           ModuleName="Iot",
           ResourceId=new Guid("21e2e09d-e941-4a19-b01d-decd8783612b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("df6fa6ae-b383-40b3-b26c-fe650b986154"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_refresh",
       ModuleName="Iot",
       Name="刷新",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6e7ae841-3312-a841-f7bc-cdb2b69d6670"),
           ModuleName="Iot",
           ResourceId=new Guid("df6fa6ae-b383-40b3-b26c-fe650b986154"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c65b450d-f54f-4657-a759-9b03df125f5d"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_search",
       ModuleName="Iot",
       Name="搜索",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6e7ae841-3312-a841-f7bc-cdb2b69d6670"),
           ModuleName="Iot",
           ResourceId=new Guid("c65b450d-f54f-4657-a759-9b03df125f5d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9618bf96-34b0-4f03-aa7e-9c240a4ed631"),
       IsDeleted=false,
       IsLocked=false,
       Key="iot_product_update",
       ModuleName="Iot",
       Name="更新",
       Order=0,
       ParentId=new Guid("13808953-8a68-42c1-a9ef-a3f18dc9e6de"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03bde8d8-5b0c-bffa-7456-7c808676f815"),
           ModuleName="Iot",
           ResourceId=new Guid("9618bf96-34b0-4f03-aa7e-9c240a4ed631"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a1e3be2f-1f1b-f33d-4692-5a9f0a6ddd2b"),
           ModuleName="Iot",
           ResourceId=new Guid("9618bf96-34b0-4f03-aa7e-9c240a4ed631"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b4d0f205-919e-1a45-bfdf-68d7a751781d"),
           ModuleName="Iot",
           ResourceId=new Guid("9618bf96-34b0-4f03-aa7e-9c240a4ed631"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
