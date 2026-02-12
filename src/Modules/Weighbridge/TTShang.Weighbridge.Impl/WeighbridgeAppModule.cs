// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;
using TTShang.Core.SystemAsset.Dtos;
using TTShang.Core.SystemAsset.Enums;
using TTShang.Core.UserCenter.Dtos;

namespace TTShang.Weighbridge.Impl
{
    /// <summary>
    /// WeighbridgeApp
    /// </summary>
    public class WeighbridgeAppModule : IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "WeighbridgeApp";

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get
            {
                return "1.0.16";
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description => "电子地磅App";

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            return new[] {
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_login",
       ModuleName="WeighbridgeApp",
       Name="登录",
       Order=0,
       ParentId=new Guid("089e58f2-a0ce-4f0c-92d7-b075f8320034"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("20017bbd-fefe-d7f3-4343-fac4fe00028f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0b6bde9e-19eb-f0ab-20fc-c830d1fc2b98"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f58e1b81-3a1b-8418-1fd8-a34dc623df14"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f0f58308-39b5-b502-5055-a4db35fcadf2"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("797a8ea4-7887-c886-66b4-4fcf77369bc0"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("35891c81-89c3-6ea5-9095-8ecfd44d9e14"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8096d344-eeb2-1f32-f434-6badbd524e87"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("cbec2c01-ec61-489a-9f9d-da3abeaa10f4"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("089e58f2-a0ce-4f0c-92d7-b075f8320034"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_root",
       ModuleName="WeighbridgeApp",
       Name="WeighbridgeApp节点",
       Order=5,
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("75d3bfac-66cb-6160-62f8-78140fb6339d"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("089e58f2-a0ce-4f0c-92d7-b075f8320034"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Root,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user",
       ModuleName="WeighbridgeApp",
       Name="我的",
       Order=10,
       ParentId=new Guid("089e58f2-a0ce-4f0c-92d7-b075f8320034"),
       Path="/user",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("f2fc73fe-e978-4dcd-b6be-ff9290a77593"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_change_password",
       ModuleName="WeighbridgeApp",
       Name="修改密码",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       Path="/change-password",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("95784ea0-7d99-cb7b-9136-6ecce781b5ef"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("f2fc73fe-e978-4dcd-b6be-ff9290a77593"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7a19a590-e402-4b67-81f9-bf7e9fccfc83"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_commodity_manager",
       ModuleName="WeighbridgeApp",
       Name="货物管理",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       Path="/commodity-manager",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1c421560-e61f-4193-9fab-be8087985faa"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_commodity_manager_add",
       ModuleName="WeighbridgeApp",
       Name="添加",
       Order=0,
       ParentId=new Guid("7a19a590-e402-4b67-81f9-bf7e9fccfc83"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("76e2d158-395e-93f0-f489-858f450efa48"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("1c421560-e61f-4193-9fab-be8087985faa"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("77c03fd6-42b4-7e64-3058-07e90b05f194"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("1c421560-e61f-4193-9fab-be8087985faa"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2cc0f05c-2973-4b3b-ac1a-590dae8e5127"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_commodity_manager_delete",
       ModuleName="WeighbridgeApp",
       Name="删除",
       Order=0,
       ParentId=new Guid("7a19a590-e402-4b67-81f9-bf7e9fccfc83"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3567b546-e09d-e158-5e6f-7a1c61226cf2"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("2cc0f05c-2973-4b3b-ac1a-590dae8e5127"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f083d0e3-dec2-f50c-4151-5ca4ba79433a"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("2cc0f05c-2973-4b3b-ac1a-590dae8e5127"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("936e3059-2398-4c8a-a019-33cb832cfad5"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_commodity_manager_list",
       ModuleName="WeighbridgeApp",
       Name="列表",
       Order=0,
       ParentId=new Guid("7a19a590-e402-4b67-81f9-bf7e9fccfc83"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("01ca5e0d-8330-e32e-cc34-0b032883d1a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("936e3059-2398-4c8a-a019-33cb832cfad5"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0fe9b29b-cbfd-7689-48d3-0c2e85451877"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("936e3059-2398-4c8a-a019-33cb832cfad5"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("df72df7c-cc81-423b-8115-38c38d8432e5"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_commodity_manager_update",
       ModuleName="WeighbridgeApp",
       Name="更新",
       Order=0,
       ParentId=new Guid("7a19a590-e402-4b67-81f9-bf7e9fccfc83"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("76e2d158-395e-93f0-f489-858f450efa48"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("df72df7c-cc81-423b-8115-38c38d8432e5"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a3ceed8d-65bc-d3f0-9133-d6cbdc0b4bb7"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("df72df7c-cc81-423b-8115-38c38d8432e5"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("37105e30-7436-4204-b5de-2c2fce0c7c7b"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager",
       ModuleName="WeighbridgeApp",
       Name="设备管理",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       Path="/device-manager",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5c756f11-d959-2212-1875-dd4ca9042b90"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("37105e30-7436-4204-b5de-2c2fce0c7c7b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e3818245-9807-405d-a9fd-bcefcd73e79b"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager_bind",
       ModuleName="WeighbridgeApp",
       Name="绑定设备",
       Order=0,
       ParentId=new Guid("37105e30-7436-4204-b5de-2c2fce0c7c7b"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03d633c8-be42-cb92-ab67-27b10bc79b9b"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("e3818245-9807-405d-a9fd-bcefcd73e79b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager_config",
       ModuleName="WeighbridgeApp",
       Name="设置",
       Order=0,
       ParentId=new Guid("37105e30-7436-4204-b5de-2c2fce0c7c7b"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e6fbb9db-6541-2518-dfe8-997ce7104204"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a234195a-efab-ab76-e017-99489265ce6c"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4a0c4bf4-23b1-3f24-cade-26b1922dbf1d"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a1e6947e-e961-f732-e028-5511b88916bd"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9eff0687-5672-3fd8-5b29-4d65ec867919"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5e42e48c-2c82-2de2-0571-3e829375f4fc"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5b7a84c2-5ad1-cf2f-a0ef-4a22fcfc70d4"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4fc5c9a3-ee5f-59fb-fa03-7e2c46ed96ce"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4cb75d7b-e926-dff8-ca07-159e15068fe9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3cff8eef-588d-b87e-5683-56c094bfd058"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("014e175d-d993-bb30-3365-a7eb5f915d9f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    },
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4a40077c-af2d-4349-9905-cca59f69a267"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager_config_base",
       ModuleName="WeighbridgeApp",
       Name="基础配置",
       Order=0,
       ParentId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("07cca08e-ad08-4f77-bd78-b9d8806165c5"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager_config_diy",
       ModuleName="WeighbridgeApp",
       Name="自定义配置",
       Order=0,
       ParentId=new Guid("fdb41319-5749-4df4-8a4f-87d88b655354"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("08dd8ca0-b60c-4f05-81fd-f016079cf8f2"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("af1e2de4-8de1-435e-a958-bed852563fdb"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("20caaef8-eb78-fdc4-963c-1fbc61712952"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("af1e2de4-8de1-435e-a958-bed852563fdb"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("82e165ca-231a-2e60-103f-f7c4b16ed025"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("af1e2de4-8de1-435e-a958-bed852563fdb"),
        }

    }
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("aed7ea4c-55fa-4eb7-9f89-57e026d35b8f"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_device_manager_update_alias",
       ModuleName="WeighbridgeApp",
       Name="修改别名",
       Order=0,
       ParentId=new Guid("37105e30-7436-4204-b5de-2c2fce0c7c7b"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e98d77be-9280-730a-01b6-51f6f1c3f0ea"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("aed7ea4c-55fa-4eb7-9f89-57e026d35b8f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4a9ed2f9-c80b-4f10-8937-2d2fa7294d78"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_print_settings",
       ModuleName="WeighbridgeApp",
       Name="打印设置",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       Path="/print-settings",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e5195606-9fe8-41ea-9b97-2aced6773bdb"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_print_settings_connect",
       ModuleName="WeighbridgeApp",
       Name="连接打印机",
       Order=0,
       ParentId=new Guid("4a9ed2f9-c80b-4f10-8937-2d2fa7294d78"),
       Path="/printer-connect",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("93c30591-cb42-4fef-bc3a-06e30fe77450"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_print_settings_connect_test",
       ModuleName="WeighbridgeApp",
       Name="测试打印",
       Order=0,
       ParentId=new Guid("e5195606-9fe8-41ea-9b97-2aced6773bdb"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5d51a13d-bdbc-829d-cfb6-1a6c2b5999e5"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("93c30591-cb42-4fef-bc3a-06e30fe77450"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b3073549-1c9d-4bb9-9796-d5142610181f"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_print_settings_template",
       ModuleName="WeighbridgeApp",
       Name="模板设置",
       Order=0,
       ParentId=new Guid("4a9ed2f9-c80b-4f10-8937-2d2fa7294d78"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager",
       ModuleName="WeighbridgeApp",
       Name="地磅管理",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       Path="/weighbridge-manager",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a61eccee-7fce-469a-bfe8-2a6404d424bf"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_add",
       ModuleName="WeighbridgeApp",
       Name="添加",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03d633c8-be42-cb92-ab67-27b10bc79b9b"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("a61eccee-7fce-469a-bfe8-2a6404d424bf"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a7171bf8-be5c-85b4-028a-267ad540a68a"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("a61eccee-7fce-469a-bfe8-2a6404d424bf"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ba00a204-67b0-e0f8-2981-462585f1893f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("a61eccee-7fce-469a-bfe8-2a6404d424bf"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("52d4afd8-91b7-458d-bb0b-1bf498ecbb2a"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_delete",
       ModuleName="WeighbridgeApp",
       Name="删除",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d6dec29b-15d8-676b-205d-04959212b933"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("52d4afd8-91b7-458d-bb0b-1bf498ecbb2a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("316db6ae-d27c-4b51-8739-57556999af5a"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_detail",
       ModuleName="WeighbridgeApp",
       Name="详情",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4a347b5e-655b-b29e-5316-f5b8282ec9a6"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("316db6ae-d27c-4b51-8739-57556999af5a"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdef10fb-21df-ddca-3bcb-01a9883162a3"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("316db6ae-d27c-4b51-8739-57556999af5a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8aff837c-1a34-4f6d-8997-42d085ff6c4f"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_edit",
       ModuleName="WeighbridgeApp",
       Name="编辑",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("03d633c8-be42-cb92-ab67-27b10bc79b9b"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("8aff837c-1a34-4f6d-8997-42d085ff6c4f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a7171bf8-be5c-85b4-028a-267ad540a68a"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("8aff837c-1a34-4f6d-8997-42d085ff6c4f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ba00a204-67b0-e0f8-2981-462585f1893f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("8aff837c-1a34-4f6d-8997-42d085ff6c4f"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("772ed26b-2c46-41a8-b4c9-ac3cb999c3f0"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_enable",
       ModuleName="WeighbridgeApp",
       Name="启用",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("58fea705-3b2b-2249-0a63-cd048b637668"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("772ed26b-2c46-41a8-b4c9-ac3cb999c3f0"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("c644a7d2-675f-4668-85b0-6db37776df5d"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighbridge_manager_list",
       ModuleName="WeighbridgeApp",
       Name="列表",
       Order=0,
       ParentId=new Guid("960f588c-3834-4df7-a8ad-a4468d25a94c"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d01bbc1b-5339-ef74-ddac-f6737aeb5d40"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("c644a7d2-675f-4668-85b0-6db37776df5d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bcc756de-ef96-4918-8cc8-ca0f915ddfc7"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighing_record",
       ModuleName="WeighbridgeApp",
       Name="称重记录",
       Order=0,
       ParentId=new Guid("41508f0b-6a27-44f6-b3d4-308b2be961b3"),
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4659e186-af19-4bf9-ae3d-580cffddc264"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighing_record_detail",
       ModuleName="WeighbridgeApp",
       Name="详情",
       Order=0,
       ParentId=new Guid("bcc756de-ef96-4918-8cc8-ca0f915ddfc7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("64708eb4-a3e5-5107-8108-6e3ce147c8f0"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("4659e186-af19-4bf9-ae3d-580cffddc264"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("0252f750-8f1a-4ab2-8e87-8708165790a7"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighing_record_list",
       ModuleName="WeighbridgeApp",
       Name="列表",
       Order=0,
       ParentId=new Guid("bcc756de-ef96-4918-8cc8-ca0f915ddfc7"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("01ca5e0d-8330-e32e-cc34-0b032883d1a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("0252f750-8f1a-4ab2-8e87-8708165790a7"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9fe95e9-2d0d-12d5-0bc0-7e07824293bc"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("0252f750-8f1a-4ab2-8e87-8708165790a7"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4194bf9b-f940-42dc-978b-c97461c0c009"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighing_record_list_again",
       ModuleName="WeighbridgeApp",
       Name="再次称重",
       Order=0,
       ParentId=new Guid("4659e186-af19-4bf9-ae3d-580cffddc264"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b0c5fb8b-b8ed-4303-ab53-b290784703f6"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_user_weighing_record_list_print",
       ModuleName="WeighbridgeApp",
       Name="打印",
       Order=0,
       ParentId=new Guid("4659e186-af19-4bf9-ae3d-580cffddc264"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0c09a34f-53ed-bd67-1537-809323c9b2a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b0c5fb8b-b8ed-4303-ab53-b290784703f6"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b4217360-435a-4f55-a2fd-bfb6d1f01bcf"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge",
       ModuleName="WeighbridgeApp",
       Name="地磅",
       Order=0,
       ParentId=new Guid("089e58f2-a0ce-4f0c-92d7-b075f8320034"),
       Path="/",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_detail",
       ModuleName="WeighbridgeApp",
       Name="详情",
       Order=0,
       ParentId=new Guid("b4217360-435a-4f55-a2fd-bfb6d1f01bcf"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fdbe5202-beb3-1830-9d07-6872f3b49f03"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f73b91fc-252f-a9b6-356b-d20e00fab04f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("bc3f5210-0b2a-0cc4-cd1a-a2dfcef5fdc4"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a42891e9-f948-899e-57f3-d7fc088d3438"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("571363fc-7338-7fd9-5114-a579df187d90"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4a347b5e-655b-b29e-5316-f5b8282ec9a6"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2e1e4cd5-91db-18d2-ccaf-f821b593784b"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0fe9b29b-cbfd-7689-48d3-0c2e85451877"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("69b2e85e-5934-f5a5-8b56-8318f1146570"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("edd32cbf-acd9-4654-951f-ddcc6f65d881"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_devices",
       ModuleName="WeighbridgeApp",
       Name="设备详情",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a194b860-b325-4707-8a55-9f616fb85bcd"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_dynamic_weighing",
       ModuleName="WeighbridgeApp",
       Name="过称",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("563eb18d-ac86-4550-8233-6980bc9e6de6"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_f1_detail",
       ModuleName="WeighbridgeApp",
       Name="F1称重页面",
       Order=0,
       ParentId=new Guid("b4217360-435a-4f55-a2fd-bfb6d1f01bcf"),
       Path="/f1",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("08dd7974-d55b-4c5a-8bdd-2a17e60618ea"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("563eb18d-ac86-4550-8233-6980bc9e6de6"),
        }

    }
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9fd8493f-a349-41ef-8dc1-67fb4a0d344d"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_f1_detail_reset_to_zero",
       ModuleName="WeighbridgeApp",
       Name="清零",
       Order=0,
       ParentId=new Guid("563eb18d-ac86-4550-8233-6980bc9e6de6"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2fe4da06-3024-9a87-dcf6-71f11deabc7f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("9fd8493f-a349-41ef-8dc1-67fb4a0d344d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("39db1b93-8921-4569-a27a-295bfe5f29dc"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_f1_detail_save_record",
       ModuleName="WeighbridgeApp",
       Name="保存记录",
       Order=0,
       ParentId=new Guid("563eb18d-ac86-4550-8233-6980bc9e6de6"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cdd67bd9-8149-bf2c-dca2-8b0744a82b61"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("39db1b93-8921-4569-a27a-295bfe5f29dc"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("50102604-c45b-43a0-bb9b-dce35f87d2a2"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_list",
       ModuleName="WeighbridgeApp",
       Name="列表",
       Order=0,
       ParentId=new Guid("b4217360-435a-4f55-a2fd-bfb6d1f01bcf"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d01bbc1b-5339-ef74-ddac-f6737aeb5d40"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("50102604-c45b-43a0-bb9b-dce35f87d2a2"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("00551874-f97c-469f-a157-efe494a832cc"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_list_again",
       ModuleName="WeighbridgeApp",
       Name="再次称重",
       Order=0,
       ParentId=new Guid("ba3e26c0-b83c-4b87-bd0f-da927549d4ca"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("56d2a17b-95fa-4124-b800-25bb86ef7769"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_lpr",
       ModuleName="WeighbridgeApp",
       Name="识别车牌",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a77c8afa-221f-0917-ca40-41d8ab204c03"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("56d2a17b-95fa-4124-b800-25bb86ef7769"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bea75de5-00bf-48f4-9011-dd7463326c5b"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_netweight",
       ModuleName="WeighbridgeApp",
       Name="去皮",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("024943fc-f326-f146-fe7e-de93d8051178"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("bea75de5-00bf-48f4-9011-dd7463326c5b"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("6c7180af-bcef-4764-9877-d8b80e3c685d"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_print",
       ModuleName="WeighbridgeApp",
       Name="打印按钮",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0c09a34f-53ed-bd67-1537-809323c9b2a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("6c7180af-bcef-4764-9877-d8b80e3c685d"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9f2759dd-be7b-70c2-94e6-0b8f275d2e27"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("6c7180af-bcef-4764-9877-d8b80e3c685d"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("475f8c8e-21ab-4826-af54-f3d483cdceaa"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_record_btn",
       ModuleName="WeighbridgeApp",
       Name="记录按钮",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ba3e26c0-b83c-4b87-bd0f-da927549d4ca"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_record_btn_detail",
       ModuleName="WeighbridgeApp",
       Name="详情",
       Order=0,
       ParentId=new Guid("475f8c8e-21ab-4826-af54-f3d483cdceaa"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("64708eb4-a3e5-5107-8108-6e3ce147c8f0"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("ba3e26c0-b83c-4b87-bd0f-da927549d4ca"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("86c3bdf3-e750-4272-883d-55fd7b90e741"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_record_list",
       ModuleName="WeighbridgeApp",
       Name="记录列表",
       Order=0,
       ParentId=new Guid("475f8c8e-21ab-4826-af54-f3d483cdceaa"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("01ca5e0d-8330-e32e-cc34-0b032883d1a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("86c3bdf3-e750-4272-883d-55fd7b90e741"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9fe95e9-2d0d-12d5-0bc0-7e07824293bc"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("86c3bdf3-e750-4272-883d-55fd7b90e741"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.View,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("678d331a-8870-467f-aa87-23d4b0ce89a1"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_record_print",
       ModuleName="WeighbridgeApp",
       Name="打印",
       Order=0,
       ParentId=new Guid("ba3e26c0-b83c-4b87-bd0f-da927549d4ca"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0c09a34f-53ed-bd67-1537-809323c9b2a9"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("678d331a-8870-467f-aa87-23d4b0ce89a1"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9a401461-6c85-4939-999b-788bc1a22291"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_reset_to_zero",
       ModuleName="WeighbridgeApp",
       Name="清零",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2fe4da06-3024-9a87-dcf6-71f11deabc7f"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("9a401461-6c85-4939-999b-788bc1a22291"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ab517835-3153-4158-92e5-612889cff05e"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_save_record",
       ModuleName="WeighbridgeApp",
       Name="保存记录",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cdd67bd9-8149-bf2c-dca2-8b0744a82b61"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("ab517835-3153-4158-92e5-612889cff05e"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8b1f55ac-e361-4309-9492-c5e1dd234b8a"),
       IsDeleted=false,
       IsLocked=false,
       Key="wbg_app_weighbridge_unload",
       ModuleName="WeighbridgeApp",
       Name="卸货",
       Order=0,
       ParentId=new Guid("b796e70b-739a-41f4-b743-16bc058d2ac0"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("024943fc-f326-f146-fe7e-de93d8051178"),
           ModuleName="WeighbridgeApp",
           ResourceId=new Guid("8b1f55ac-e361-4309-9492-c5e1dd234b8a"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }};
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
                    ConfigKey="TenantWbgAppF1Logo",
                    DefaultConfigValue="",
                    Description="该租户地磅APP F1 页面 logo"
                },
                new SystemTenantConfigTemplateDto()
                {
                    ConfigKey="TenantWbgAppF1BgImg",
                    DefaultConfigValue="",
                    Description="该租户地磅APP F1 页面 背景"
                }
                ];
        }
    }
}
