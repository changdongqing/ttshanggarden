// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using TTShang.Core.Api.Impl.Dict.Services;
using TTShang.Core.Dict;
using TTShang.Core.Dict.Services;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Dict
{
    /// <summary>
    /// 模块
    /// </summary>
    public class DictServerModule : DictModule, IServerModule
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
                return [typeof(CodeService), typeof(CodeTypeService)];
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 尽量快
        /// </remarks>
        /// <returns></returns>
        public Task OnExecute(CancellationToken cancellationToken)
        {
            //启动时初始化 DictHelper 所有code 缓存
            var codeTypeService = App.GetRequiredService<ICodeTypeService>();
            return codeTypeService.RefreshDictHelperCache();
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
       Id=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code",
       ModuleName="Dict",
       Name="字典",
       Order=20,
       ParentId=new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"),
       Path="/system_manager/code_list",
       Remark="字典管理",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4b4f7b73-df18-4201-876e-b27e172f3b55"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_add",
       ModuleName="Dict",
       Name="添加字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="添加字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9cd8f55a-faf2-2baf-8c16-658759571648"),
           ModuleName="Dict",
           ResourceId=new Guid("4b4f7b73-df18-4201-876e-b27e172f3b55"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("535e5f96-a036-4a40-96af-6c03cecadcd1"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_delete",
       ModuleName="Dict",
       Name="删除字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="删除字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("378fadeb-4c51-7b40-d839-b33e1b2e291b"),
           ModuleName="Dict",
           ResourceId=new Guid("535e5f96-a036-4a40-96af-6c03cecadcd1"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("eb0596a5-89c8-ae55-2a4e-9cca39456a4e"),
           ModuleName="Dict",
           ResourceId=new Guid("535e5f96-a036-4a40-96af-6c03cecadcd1"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bedacea2-80f1-4d4a-b401-c82940f80d4c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_delete_selected",
       ModuleName="Dict",
       Name="删除选中字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="删除选中字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("87e01658-ab00-dc70-b8d9-c9355ceb7da5"),
           ModuleName="Dict",
           ResourceId=new Guid("bedacea2-80f1-4d4a-b401-c82940f80d4c"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cb1fd377-46cd-8c99-b14a-2af54bd2409b"),
           ModuleName="Dict",
           ResourceId=new Guid("bedacea2-80f1-4d4a-b401-c82940f80d4c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("36a4434a-f702-42be-a211-862d0b3b5288"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_detail",
       ModuleName="Dict",
       Name="查看字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="查看字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d6237211-e136-fc8c-eb58-5a30b4956483"),
           ModuleName="Dict",
           ResourceId=new Guid("36a4434a-f702-42be-a211-862d0b3b5288"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_download_seed_data",
       ModuleName="Dict",
       Name="生成字典种子数据",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="生成字典种子数据",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5b3849f3-86ed-9cc2-ff7d-354fb4bf56ad"),
           ModuleName="Dict",
           ResourceId=new Guid("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_edit",
       ModuleName="Dict",
       Name="编辑字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="编辑字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0fab6258-310f-4d84-f82b-2a11ec272ccd"),
           ModuleName="Dict",
           ResourceId=new Guid("520f5cec-5f33-447c-a18b-59d8db31c5e9"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("1f289163-7fb0-49d2-9165-cbb111b6f3ab"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_export",
       ModuleName="Dict",
       Name="导出字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="导出字典",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ce077774-d897-f5ec-c8df-adae504a4e5d"),
           ModuleName="Dict",
           ResourceId=new Guid("1f289163-7fb0-49d2-9165-cbb111b6f3ab"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a37b1cd8-98c4-4a93-a73e-436c138639eb"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_lock",
       ModuleName="Dict",
       Name="锁定字典",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d4e6a901-fa39-7675-504b-eb8c5d03e189"),
           ModuleName="Dict",
           ResourceId=new Guid("a37b1cd8-98c4-4a93-a73e-436c138639eb"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="tags",
       Id=new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_manager",
       ModuleName="Dict",
       Name="字典管理",
       Order=90,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d9fc6b89-25bb-458e-936f-d76eea2c680f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_refresh",
       ModuleName="Dict",
       Name="刷新字典列表",
       Order=0,
       ParentId=new Guid("d5e3497b-c624-4fde-96bd-108a33cacc6d"),
       Remark="刷新字典列表",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("239f30eb-38bd-ac82-d135-544037d07d88"),
           ModuleName="Dict",
           ResourceId=new Guid("d9fc6b89-25bb-458e-936f-d76eea2c680f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type",
       ModuleName="Dict",
       Name="字典类型",
       Order=10,
       ParentId=new Guid("b99ad8cf-68db-49aa-838f-17d57429d9c5"),
       Path="/system_manager/code_type",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_add",
       ModuleName="Dict",
       Name="添加字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("01e2e0d0-bef9-57d9-af77-3f3e38371972"),
           ModuleName="Dict",
           ResourceId=new Guid("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("7819fe8f-8d81-4d00-af2b-c53ec010c65b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_codes_manager",
       ModuleName="Dict",
       Name="管理字典类型下字典",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       Remark="功能与 字典管理->字典 相同",
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("addfa7a9-7a8c-46ce-90f1-11424f385954"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_delete",
       ModuleName="Dict",
       Name="删除字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3b67db0f-e90f-76b8-1046-7889d8d43f16"),
           ModuleName="Dict",
           ResourceId=new Guid("addfa7a9-7a8c-46ce-90f1-11424f385954"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4af116fb-5f39-81d9-0676-5bd49bfce8f9"),
           ModuleName="Dict",
           ResourceId=new Guid("addfa7a9-7a8c-46ce-90f1-11424f385954"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8e913a11-dbbe-4aa4-ad58-f12737039d83"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_delete_selected",
       ModuleName="Dict",
       Name="删除选中字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("62d2aa4d-33b8-6c7e-a36f-0ebc03b48dbb"),
           ModuleName="Dict",
           ResourceId=new Guid("8e913a11-dbbe-4aa4-ad58-f12737039d83"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("789774e2-eca9-e1e4-99b4-b671f7a9c5b1"),
           ModuleName="Dict",
           ResourceId=new Guid("8e913a11-dbbe-4aa4-ad58-f12737039d83"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cd23a5d8-6eab-4e46-a730-56b2808551c6"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_detail",
       ModuleName="Dict",
       Name="查看字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("25c8784a-cf96-90e6-a2e1-8d95cad03095"),
           ModuleName="Dict",
           ResourceId=new Guid("cd23a5d8-6eab-4e46-a730-56b2808551c6"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5676c9b3-2d06-4817-9614-4a34230bb05e"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_download_seed_data",
       ModuleName="Dict",
       Name="生成字典类型种子数据",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d0dc58ea-186f-7449-d8f6-22cf16a5f828"),
           ModuleName="Dict",
           ResourceId=new Guid("5676c9b3-2d06-4817-9614-4a34230bb05e"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e1ab080c-b598-4c1c-9afa-45681f90f1e3"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_edit",
       ModuleName="Dict",
       Name="编辑字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("164bd874-2477-54a5-50d7-728fe04dea1b"),
           ModuleName="Dict",
           ResourceId=new Guid("e1ab080c-b598-4c1c-9afa-45681f90f1e3"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_export",
       ModuleName="Dict",
       Name="导出字典类型",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("506b38ee-cc1c-40e1-0a82-d55aee57989d"),
           ModuleName="Dict",
           ResourceId=new Guid("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4e582063-f524-4ce2-9417-ac2cd957332d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_refresh",
       ModuleName="Dict",
       Name="刷新字典类型列表",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("333996cf-36e0-20cd-f654-e34b0f7a0ed1"),
           ModuleName="Dict",
           ResourceId=new Guid("4e582063-f524-4ce2-9417-ac2cd957332d"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("2a3f7c64-3ee9-473e-837d-5f443089c886"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_code_type_refresh_code_util_cache",
       ModuleName="Dict",
       Name="刷新字典工具缓存",
       Order=0,
       ParentId=new Guid("2eacd369-94ea-4e12-bf9e-744ae355e941"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f9767fd3-f364-5afc-57d8-3b0db96ce02b"),
           ModuleName="Dict",
           ResourceId=new Guid("2a3f7c64-3ee9-473e-837d-5f443089c886"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

};
        }
    }
}
