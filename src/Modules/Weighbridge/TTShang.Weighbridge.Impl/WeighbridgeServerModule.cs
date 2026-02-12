// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;
using TTShang.Core.Enums;
using TTShang.Core.Module;
using TTShang.Core.SystemAsset.Dtos;
using TTShang.Core.SystemAsset.Enums;
using TTShang.Core.UserCenter.Dtos;

namespace TTShang.Weighbridge.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class WeighbridgeServerModule : WeighbridgeModule, IServerModule
    {

        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        public CodeTypeDto[]? RegisterDic()
        {
            return [
                new CodeTypeDto()
                {
                   CodeTypeName="车辆轴数",
                   CodeTypeValue="number_of_axles",
                   CreateBy="1",
                   CreatedTime=DateTimeOffset.Parse("2024-07-21 16:40:31"),
                   CreateIdentityType=IdentityType.User,
                   Id=4,
                   IsDeleted=false,
                   IsLocked=false,
                   Remark="车辆轴数基础数据",
                   Codes=[

    new CodeDto()
    {
       CodeName="二轴",
       CodeTypeId=4,
       CodeValue="2",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:42:55"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":15}",
       Id=16,
       IsDeleted=false,
       IsLocked=false,
       Order=20,
    }
,
    new CodeDto()
    {
       CodeName="三轴",
       CodeTypeId=4,
       CodeValue="3",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:43:18"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":20}",
       Id=17,
       IsDeleted=false,
       IsLocked=false,
       Order=30,
    }
,
    new CodeDto()
    {
       CodeName="四轴",
       CodeTypeId=4,
       CodeValue="4",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:43:48"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":50}",
       Id=18,
       IsDeleted=false,
       IsLocked=false,
       Order=40,
    }
,
    new CodeDto()
    {
       CodeName="五轴",
       CodeTypeId=4,
       CodeValue="5",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:44:11"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":60}",
       Id=19,
       IsDeleted=false,
       IsLocked=false,
       Order=50,
    }
,
    new CodeDto()
    {
       CodeName="六轴",
       CodeTypeId=4,
       CodeValue="6",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:44:45"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":80}",
       Id=20,
       IsDeleted=false,
       IsLocked=false,
       Order=60,
    }
,
    new CodeDto()
    {
       CodeName="其它",
       CodeTypeId=4,
       CodeValue="99",
       CreateBy="1",
       CreatedTime=DateTimeOffset.Parse("2024-07-21 16:45:40"),
       CreateIdentityType=IdentityType.User,
       ExtendParams="{\"maximumLoad\":0}",
       Id=21,
       IsDeleted=false,
       IsLocked=false,
       Order=100,
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
       Icon="download",
       Id=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge",
       ModuleName="Weighbridge",
       Name="电子地磅",
       Order=400,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bb6984e3-3da9-42b9-b389-71dbe5f9c86b"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("77c03fd6-42b4-7e64-3058-07e90b05f194"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("bb6984e3-3da9-42b9-b389-71dbe5f9c86b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("976f699e-713d-44de-a491-42a42a995aeb"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3ff9b587-327c-510a-3448-1f218fb083b3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("976f699e-713d-44de-a491-42a42a995aeb"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f083d0e3-dec2-f50c-4151-5ca4ba79433a"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("976f699e-713d-44de-a491-42a42a995aeb"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2e11ab43-a4b5-4bba-9769-a30cca2453a3"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("708e9d53-36fb-bccf-8e95-fe818137fc83"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2e11ab43-a4b5-4bba-9769-a30cca2453a3"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b815eb0d-6a25-32dc-e98c-9811c3cb3829"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2e11ab43-a4b5-4bba-9769-a30cca2453a3"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("611b5d91-7b48-4fa6-a180-931177c92207"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7f121f8f-fbe4-bfa1-4687-481191d9b9ce"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("611b5d91-7b48-4fa6-a180-931177c92207"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("893b5586-a094-44a5-87bf-45877aa5ab60"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_export",
       ModuleName="Weighbridge",
       Name="导出",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6a4c2932-1783-6c4e-3906-9fd06004be20"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("893b5586-a094-44a5-87bf-45877aa5ab60"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_index",
       ModuleName="Weighbridge",
       Name="货物管理",
       Order=30,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/commodity",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ecd218e6-88e9-4816-b51f-daefeb4200b5"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ce465ac1-3277-6621-9d1c-abeb4c4dc1c6"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("ecd218e6-88e9-4816-b51f-daefeb4200b5"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c1e61795-1121-4f4a-99a0-25cb5780e9d1"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0fe9b29b-cbfd-7689-48d3-0c2e85451877"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("c1e61795-1121-4f4a-99a0-25cb5780e9d1"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("54b560c4-57a6-4279-8fac-9addd4d74c96"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0fe9b29b-cbfd-7689-48d3-0c2e85451877"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("54b560c4-57a6-4279-8fac-9addd4d74c96"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9cc49881-3519-4259-b108-d1324afbc3fb"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_commodity_update",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("a08ac9a8-f8e2-4fa1-a5d7-f7ed649dfaa9"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7f121f8f-fbe4-bfa1-4687-481191d9b9ce"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("9cc49881-3519-4259-b108-d1324afbc3fb"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a3ceed8d-65bc-d3f0-9133-d6cbdc0b4bb7"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("9cc49881-3519-4259-b108-d1324afbc3fb"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2a8e647f-a7f5-483e-9c1e-e23dfd5bde21"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5e97a2c5-8e31-0854-02a5-e57e16977d27"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2a8e647f-a7f5-483e-9c1e-e23dfd5bde21"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f826bf9a-16cd-ab4d-1940-d874eaca3975"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2a8e647f-a7f5-483e-9c1e-e23dfd5bde21"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdef10fb-21df-ddca-3bcb-01a9883162a3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2a8e647f-a7f5-483e-9c1e-e23dfd5bde21"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d37d3efe-7981-45e9-9984-3619f769aedd"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1e985fba-2185-f87a-6c9e-f107df61f351"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d37d3efe-7981-45e9-9984-3619f769aedd"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d6dec29b-15d8-676b-205d-04959212b933"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d37d3efe-7981-45e9-9984-3619f769aedd"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("26f40760-8154-4110-bd2e-f02c00a80a84"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b461d8c4-9b29-33b6-7cdb-9d2708eb030d"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("26f40760-8154-4110-bd2e-f02c00a80a84"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c06b635c-c589-da3a-b1ab-6498cdf9f5b0"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("26f40760-8154-4110-bd2e-f02c00a80a84"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("6968a35f-61e0-46a5-a13c-80d111a0e03f"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9462cbf3-6808-5a28-5e0e-420a751b04ab"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("6968a35f-61e0-46a5-a13c-80d111a0e03f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdef10fb-21df-ddca-3bcb-01a9883162a3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("6968a35f-61e0-46a5-a13c-80d111a0e03f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("42a9c5b6-5032-490f-bcd6-7dd0c433e0d0"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_export",
       ModuleName="Weighbridge",
       Name="导出",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8df8f478-72de-5dd2-d034-c9ed9ed910be"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("42a9c5b6-5032-490f-bcd6-7dd0c433e0d0"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_index",
       ModuleName="Weighbridge",
       Name="地磅配置",
       Order=8,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/weighbridge_config",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b2b9f07c-ee99-47c1-b077-9b2072958d19"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8005b20f-8f07-8213-f1af-31a5456a2c9d"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("b2b9f07c-ee99-47c1-b077-9b2072958d19"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2836ba91-249a-4050-9643-e769d6bb4dcc"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d01bbc1b-5339-ef74-ddac-f6737aeb5d40"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2836ba91-249a-4050-9643-e769d6bb4dcc"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdef10fb-21df-ddca-3bcb-01a9883162a3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2836ba91-249a-4050-9643-e769d6bb4dcc"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5bf9295f-1756-4f01-adad-471158a3b2dc"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d01bbc1b-5339-ef74-ddac-f6737aeb5d40"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("5bf9295f-1756-4f01-adad-471158a3b2dc"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("37a5c7fb-a6ea-47ba-a06f-0b4fc965e201"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_config_update",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("e1afd0ca-a6df-45a1-89cb-81dc1eaa53c8"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5025cfb9-5c9b-7e6c-1887-64a0c019b240"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("37a5c7fb-a6ea-47ba-a06f-0b4fc965e201"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5e97a2c5-8e31-0854-02a5-e57e16977d27"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("37a5c7fb-a6ea-47ba-a06f-0b4fc965e201"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdef10fb-21df-ddca-3bcb-01a9883162a3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("37a5c7fb-a6ea-47ba-a06f-0b4fc965e201"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_control",
       ModuleName="Weighbridge",
       Name="地磅控制",
       Order=10,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/weighbridge_contorl",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1f4b4836-ecc4-cf2d-9c18-fd0736f85fc6"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7f6f05be-ee88-6ad6-da5b-15314867f912"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c365d006-2964-2d5d-6264-fdba4e5a046f"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e3082de4-d4d3-48f9-e922-c7785d27d616"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("efb622a0-4796-e757-6af8-374d8285fc2c"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("62edee8b-c17e-4479-9193-af25eee7369e"),
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
       Id=new Guid("86538d43-64a2-4280-b71a-663f63572f0a"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_home",
       ModuleName="Weighbridge",
       Name="地磅首页",
       Order=0,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/home",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("019e1bc7-fb62-88d9-2f69-fde2f96b1869"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("86538d43-64a2-4280-b71a-663f63572f0a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("80bd85a7-3c37-f3cc-39eb-5647a5526e94"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("86538d43-64a2-4280-b71a-663f63572f0a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9205a940-11e3-1c19-a1a4-eb3cb0c40c8b"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("86538d43-64a2-4280-b71a-663f63572f0a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9769e3a-59cc-40b4-62b3-08d8239d26f5"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("86538d43-64a2-4280-b71a-663f63572f0a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2d055c2e-0f3b-42fa-8a10-b91d708c2ee6"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("431863ae-277c-a841-b627-8ff63bf81061"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2d055c2e-0f3b-42fa-8a10-b91d708c2ee6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4820045b-ebde-467d-aeb1-723ca29f845c"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("53b72a4a-593f-0510-106c-a82a119e301a"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4820045b-ebde-467d-aeb1-723ca29f845c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("88346efb-5c65-fd99-554d-2af8f508d074"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4820045b-ebde-467d-aeb1-723ca29f845c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3692673a-707a-48b6-8368-8a8bfb91e0ee"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("691b95a1-11c2-f250-2490-dd8550dc898b"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("3692673a-707a-48b6-8368-8a8bfb91e0ee"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("97c0a15b-4a60-eb38-b35e-566a314a5965"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("3692673a-707a-48b6-8368-8a8bfb91e0ee"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c38b4a21-5b62-4fe6-8255-0ff0467b865b"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e4285d1b-9d8b-a635-8d32-789041acb882"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("c38b4a21-5b62-4fe6-8255-0ff0467b865b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d99b2abc-5836-438a-a37f-d84aa5584e5d"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_edit",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2a71a1ae-7ea2-7a7b-cdf3-d1acedd48206"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d99b2abc-5836-438a-a37f-d84aa5584e5d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=true,
       Id=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_index",
       ModuleName="Weighbridge",
       Name="车辆类型",
       Order=5,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/vehicle-type",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cc93656c-f12c-4608-a239-5f9992a4087c"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("971d799a-f526-aac6-2842-718e93499a33"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("cc93656c-f12c-4608-a239-5f9992a4087c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("885c5889-5979-44a5-998c-d3181dcca1a2"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e70d3e4b-50c3-e390-8b14-d0293633d7c6"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("885c5889-5979-44a5-998c-d3181dcca1a2"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("69df9edb-e491-4edb-a7a0-0e2fa96a9b6c"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_type_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("b53a00a6-495a-4854-92a6-ced745e68ea7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e70d3e4b-50c3-e390-8b14-d0293633d7c6"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("69df9edb-e491-4edb-a7a0-0e2fa96a9b6c"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b5ada07d-83e7-41e1-a51c-3d095b412658"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("82b40572-25e2-4e86-7f35-25345f4acb93"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("b5ada07d-83e7-41e1-a51c-3d095b412658"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("83b4c38e-79b9-1519-0ec4-df2ec3c8afba"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("b5ada07d-83e7-41e1-a51c-3d095b412658"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("f6e581ca-1e5e-45e4-8932-185519c53628"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9403f6e2-d3c6-c268-16b6-07d350dd2b7a"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("f6e581ca-1e5e-45e4-8932-185519c53628"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("dfecad61-fc0e-760a-63f3-4a0c70cd3236"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("f6e581ca-1e5e-45e4-8932-185519c53628"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e8e7dfa6-7ddf-4d8f-afdd-ab66149a4f61"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("63794e2b-a1e7-dc5c-2d22-cd5f2b093139"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("e8e7dfa6-7ddf-4d8f-afdd-ab66149a4f61"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a6893903-81a1-689d-cc5d-04603bef94c4"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("e8e7dfa6-7ddf-4d8f-afdd-ab66149a4f61"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("20181230-9ddb-441b-b301-09dfb1cbce83"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2e8e04c9-b266-b451-62ef-b238dca1354f"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("20181230-9ddb-441b-b301-09dfb1cbce83"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_index",
       ModuleName="Weighbridge",
       Name="车辆配置",
       Order=50,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/vehicle-weighing-config",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("85909a58-d687-4c8b-8120-869295ba215b"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("50dd695c-4790-274f-9ebf-41b707249944"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("85909a58-d687-4c8b-8120-869295ba215b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("0ab692ec-0844-4c3c-9c88-c18a9ac862c0"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("44272012-5423-8d62-3694-42ac1485c085"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("0ab692ec-0844-4c3c-9c88-c18a9ac862c0"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9444ba72-ee1b-4e80-afea-9840452304f6"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("44272012-5423-8d62-3694-42ac1485c085"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("9444ba72-ee1b-4e80-afea-9840452304f6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4f2eb581-c79b-4d1d-93cc-535eccf98b4a"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_vehicle_weighing_config_update",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("a4417c1a-c798-4d7c-aed1-225ae255561c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2e8e04c9-b266-b451-62ef-b238dca1354f"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4f2eb581-c79b-4d1d-93cc-535eccf98b4a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3bd233a0-a3f3-f12b-c377-3e7947aad39d"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4f2eb581-c79b-4d1d-93cc-535eccf98b4a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("83b4c38e-79b9-1519-0ec4-df2ec3c8afba"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4f2eb581-c79b-4d1d-93cc-535eccf98b4a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1e5b9edf-f481-4474-a680-1f0f1832c2f5"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("82e165ca-231a-2e60-103f-f7c4b16ed025"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("1e5b9edf-f481-4474-a680-1f0f1832c2f5"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("94576df8-0c0d-f6e7-e21c-77ded6f307c1"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("1e5b9edf-f481-4474-a680-1f0f1832c2f5"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("319a341e-105b-4282-8a7e-328059d363e6"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5f3563bf-d0f5-5a71-60be-22ff2db39660"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("319a341e-105b-4282-8a7e-328059d363e6"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("64921632-2011-b129-87e0-b7618cc929f6"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("319a341e-105b-4282-8a7e-328059d363e6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("772d6b79-bc3b-4cf8-a336-954e6bcba38d"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8f82a6d3-beec-c951-5983-5becf0b075ec"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("772d6b79-bc3b-4cf8-a336-954e6bcba38d"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cc9f675f-4d12-a9cb-8d19-8074d80fe679"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("772d6b79-bc3b-4cf8-a336-954e6bcba38d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a04a7db0-d77d-443e-a727-282742c59782"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ea706259-ede9-cad9-2309-76a8aaa2f2c0"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("a04a7db0-d77d-443e-a727-282742c59782"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_index",
       ModuleName="Weighbridge",
       Name="地磅设备配置",
       Order=60,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/weighbridge-device-config",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d793fbf2-02dc-4a49-8660-1d0c1e46b64e"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2cb4e37c-3aec-8a2a-2547-5f3e390d1af9"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d793fbf2-02dc-4a49-8660-1d0c1e46b64e"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ab34342f-cd2f-44e3-9625-48775dde7491"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("510d14ab-0f67-b61f-a552-adbf686f8aae"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("ab34342f-cd2f-44e3-9625-48775dde7491"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1af53197-c5f7-40a2-a224-f115e1d39d07"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("510d14ab-0f67-b61f-a552-adbf686f8aae"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("1af53197-c5f7-40a2-a224-f115e1d39d07"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4b5c3b58-c878-42e4-88b6-7dc2882003ac"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighbridge_device_config_update",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("187734d0-35b1-4bd8-b520-f73a775e5795"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("20caaef8-eb78-fdc4-963c-1fbc61712952"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4b5c3b58-c878-42e4-88b6-7dc2882003ac"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("94576df8-0c0d-f6e7-e21c-77ded6f307c1"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4b5c3b58-c878-42e4-88b6-7dc2882003ac"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ea706259-ede9-cad9-2309-76a8aaa2f2c0"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("4b5c3b58-c878-42e4-88b6-7dc2882003ac"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7a635c32-5b7b-4bf8-8880-16ff5fc13421"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_add",
       ModuleName="Weighbridge",
       Name="添加",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a89459bd-5f9e-576c-e277-a6b8299fc645"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("7a635c32-5b7b-4bf8-8880-16ff5fc13421"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("efb622a0-4796-e757-6af8-374d8285fc2c"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("7a635c32-5b7b-4bf8-8880-16ff5fc13421"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2da6d663-db8b-416b-99c7-51e4c4551741"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_delete",
       ModuleName="Weighbridge",
       Name="删除",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0ff596c9-bac7-f228-bcc9-3c76689e65ab"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2da6d663-db8b-416b-99c7-51e4c4551741"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("81985612-ea89-1803-79ad-64693d9fe390"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("2da6d663-db8b-416b-99c7-51e4c4551741"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("87dd0cc0-5c66-444d-a72b-d160677073d4"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_delete_selected",
       ModuleName="Weighbridge",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9d883df3-ce2b-124d-ac96-40d682737891"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("87dd0cc0-5c66-444d-a72b-d160677073d4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ef19d69e-6146-bbec-8f9d-b03d9a8678dd"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("87dd0cc0-5c66-444d-a72b-d160677073d4"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d9c67159-68a5-4b18-8a50-8f503d90e847"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_detail",
       ModuleName="Weighbridge",
       Name="详情",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("64708eb4-a3e5-5107-8108-6e3ce147c8f0"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d9c67159-68a5-4b18-8a50-8f503d90e847"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("efb622a0-4796-e757-6af8-374d8285fc2c"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("d9c67159-68a5-4b18-8a50-8f503d90e847"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_index",
       ModuleName="Weighbridge",
       Name="称重记录",
       Order=20,
       ParentId=new Guid("427069e5-a6ba-4fd1-95bf-35d7dec7c848"),
       Path="/weighbridge/weighing-record",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    },    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("6f3b0067-d210-4f9f-9269-3f2dd071d807"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_log_detail",
       ModuleName="Weighbridge",
       Name="称重记录日志",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e87f9fad-e3d3-445a-8404-72f012ca800d"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_lock",
       ModuleName="Weighbridge",
       Name="锁定",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c2dcc53b-1cbc-dbe3-c304-4b8e1d851491"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("e87f9fad-e3d3-445a-8404-72f012ca800d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e60197dc-5ca5-4383-8a84-7c8180ab13ca"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_refresh",
       ModuleName="Weighbridge",
       Name="刷新",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9fe95e9-2d0d-12d5-0bc0-7e07824293bc"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("e60197dc-5ca5-4383-8a84-7c8180ab13ca"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("09a965f3-4eba-443d-959c-55dc5f8c1618"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_search",
       ModuleName="Weighbridge",
       Name="搜索",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9fe95e9-2d0d-12d5-0bc0-7e07824293bc"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("09a965f3-4eba-443d-959c-55dc5f8c1618"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5a43c499-afe6-41f6-a049-ba7f42ce9a4f"),
       IsDeleted=false,
       IsLocked=false,
       Key="weighbridge_weighing_record_update",
       ModuleName="Weighbridge",
       Name="更新",
       Order=0,
       ParentId=new Guid("86f18447-a72e-4c7f-a357-855a42c783f4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e1c18228-ea7c-fc42-fd02-8c72ff0744a3"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("5a43c499-afe6-41f6-a049-ba7f42ce9a4f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("efb622a0-4796-e757-6af8-374d8285fc2c"),
           ModuleName="Weighbridge",
           ResourceId=new Guid("5a43c499-afe6-41f6-a049-ba7f42ce9a4f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }

    }
}
