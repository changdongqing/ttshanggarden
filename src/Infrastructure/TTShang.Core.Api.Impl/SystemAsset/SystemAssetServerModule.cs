// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.SystemAsset.Services;
using TTShang.Core.Module;
using TTShang.Core.SystemAsset;

namespace TTShang.Core.Api.Impl.SystemAsset
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SystemAssetServerModule : SystemAssetModule, IServerModule
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
                return [typeof(FunctionService), typeof(ResourceFunctionService), typeof(ResourceService)];
            }
        }

        /// <summary>
        /// 注册资源
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            List<ResourceDto> resourceList = new List<ResourceDto>();
            //资源
            resourceList.AddRange(new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="menu",
       Id=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource",
       ModuleName="SystemAsset",
       Name="资源管理",
       Order=30,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_asset/resource",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4f167e5c-48d6-c41e-5241-eed9f85fd830"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_add",
       ModuleName="SystemAsset",
       Name="添加资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="添加资源",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("342ec357-3613-7a06-1dc1-d24b5d5f7e2f"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),
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
       Id=new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_add_children",
       ModuleName="SystemAsset",
       Name="添加子资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("342ec357-3613-7a06-1dc1-d24b5d5f7e2f"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("c18d4928-35d2-4085-aec9-379d00bcfd8f"),
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
       Id=new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_delete",
       ModuleName="SystemAsset",
       Name="删除资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="删除资源",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e7b1e1e9-5555-6195-b190-298d448ce249"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fc2782f1-e003-f8ad-3149-be2d976caea6"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("97a7d440-b7fe-4af6-a8a1-18846c48828b"),
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
       Id=new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_delete_selected",
       ModuleName="SystemAsset",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="删除选中",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("8739625d-da5a-1f1e-e8b4-d40bc4b92bbc"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ab74897d-bbc1-2c6b-a18b-f13a4f316c62"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("08ae2764-e551-45d2-9da7-49648481a8e0"),
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
       Id=new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_detail",
       ModuleName="SystemAsset",
       Name="查看资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="查看资源",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7f064084-7b9e-42d9-1f9d-05397475fbed"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),
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
       Id=new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_download_seed_data",
       ModuleName="SystemAsset",
       Name="导出种子数据",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7c4a73fa-66ff-56f9-a3ec-85008f4e4076"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("859aa714-67c7-4414-bc96-9de5b7aec2c4"),
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
       Id=new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_edit",
       ModuleName="SystemAsset",
       Name="编辑资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0c9435ce-2a4b-7105-af0b-db9865f44529"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("dec04485-3dab-4251-b7b8-1044e749a51e"),
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
       Id=new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_function_add_page_show",
       ModuleName="SystemAsset",
       Name="显示可选接口",
       Order=0,
       ParentId=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       Path="",
       Remark="显示可选接口",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("ed22e08c-0216-9809-de86-0dbaae260ab2"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("c4991844-d3b4-4f9a-9c90-c13114515796"),
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
       Id=new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_function_binding",
       ModuleName="SystemAsset",
       Name="绑定资源接口关系",
       Order=0,
       ParentId=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4dac4f8b-9540-2935-085d-365fcc1c31dd"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f3513df0-12ca-d04a-1def-97ce3fc4aad3"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),
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
       Id=new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_function_delete_selected",
       ModuleName="SystemAsset",
       Name="删除选中资源接口关系",
       Order=0,
       ParentId=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6330982d-c6f8-963e-d5dd-bc745b8b0e64"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("4f943ed1-997a-485f-9b54-9824b4ac285c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("64346edf-1390-4a90-bc63-93f322ed6c8f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_function_download_seed_data",
       ModuleName="SystemAsset",
       Name="获取种子数据",
       Order=0,
       ParentId=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f98658cb-8c2a-36c6-b1f6-9636ae4741b0"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("64346edf-1390-4a90-bc63-93f322ed6c8f"),
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
       Id=new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_lock",
       ModuleName="SystemAsset",
       Name="锁定资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d6ee9d8c-548b-31ac-4496-1646539bcadc"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("a1958e51-06d4-4b29-9533-eae9d86c41d1"),
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
       Id=new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_refresh",
       ModuleName="SystemAsset",
       Name="刷新资源",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4f167e5c-48d6-c41e-5241-eed9f85fd830"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("365fc5c4-404e-408a-88dc-7614dffad91b"),
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
       Id=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_show_function",
       ModuleName="SystemAsset",
       Name="关联资源接口",
       Order=0,
       ParentId=new Guid("14636a9b-e6d6-436f-a0aa-0170eed08d99"),
       Path="",
       Remark="",
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_resource_show_function_1",
       ModuleName="SystemAsset",
       Name="显示已关联接口",
       Order=0,
       ParentId=new Guid("e252c0c6-0f19-4768-954c-c0d83fb96d74"),
       Path="",
       Remark="显示已关联接口",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("35feb245-2d59-242b-32f6-815a836df391"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

});
            //接口
            resourceList.AddRange(new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="api",
       Id=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function",
       ModuleName="SystemAsset",
       Name="接口管理",
       Order=40,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_asset/function",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("38efae95-649a-9ed9-d691-d616f2ce8d59"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_delete",
       ModuleName="SystemAsset",
       Name="删除接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a45427ba-a7b0-c6f4-2a9b-7645506a7ecd"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c685ec8b-6b75-2e70-bcdf-ea9a697f7308"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),
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
       Id=new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_delete_selected",
       ModuleName="SystemAsset",
       Name="删除选中接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a10d923f-8027-1fb8-844a-c445ff363e03"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cae066a8-e15c-f51f-ebbd-ea7260d46acc"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),
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
       Id=new Guid("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_detail",
       ModuleName="SystemAsset",
       Name="查看接口详情",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="查看接口详情",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("094bdd43-cf58-8eea-5042-ace47b8fbb29"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),
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
       Id=new Guid("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_download_seed_data",
       ModuleName="SystemAsset",
       Name="查看接口种子数据",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="查看接口种子数据",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b44172a8-de14-7605-3d60-b6862c81aa7a"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"),
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
       Id=new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_edit",
       ModuleName="SystemAsset",
       Name="编辑接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1ed57059-ebf2-c32b-a665-a367484200eb"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("50062351-8235-4da1-9f90-4917d0e8abe0"),
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
       Id=new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_enable_audit",
       ModuleName="SystemAsset",
       Name="锁定接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("52235a83-0fec-1275-0d91-af22d2e9f900"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("6e487179-5bb2-4ab5-80e3-58c514c9595f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_export",
       ModuleName="SystemAsset",
       Name="导出接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Remark="导出接口",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f5b53e50-a749-df9b-1ce1-c5a5376d6dce"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("4171f5aa-2ce1-40ad-b69e-59de1cd20416"),
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
       Id=new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_asset_function_refresh",
       ModuleName="SystemAsset",
       Name="刷新接口",
       Order=0,
       ParentId=new Guid("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("38efae95-649a-9ed9-d691-d616f2ce8d59"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("6ac07813-4d10-4b50-9f0c-ecd444041282"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

});
            //资源接口绑定
            resourceList.AddRange(new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="apartment",
       Id=new Guid("559c2034-5d28-441a-bbc8-9711bd13f654"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_resource_function_bind",
       ModuleName="SystemAsset",
       Name="资源接口绑定",
       Order=42,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_asset/resource_function_bind",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3a4e4fb4-12f0-4298-9022-20224f6cc0a4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_resource_function_bind_update",
       ModuleName="SystemAsset",
       Name="修改绑定信息",
       Order=0,
       ParentId=new Guid("559c2034-5d28-441a-bbc8-9711bd13f654"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("6330982d-c6f8-963e-d5dd-bc745b8b0e64"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("3a4e4fb4-12f0-4298-9022-20224f6cc0a4"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("f3513df0-12ca-d04a-1def-97ce3fc4aad3"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("3a4e4fb4-12f0-4298-9022-20224f6cc0a4"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("f87a5605-7ba9-410f-bf3e-aa1811b1265f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_resource_function_bind_view",
       ModuleName="SystemAsset",
       Name="查看绑定信息",
       Order=0,
       ParentId=new Guid("559c2034-5d28-441a-bbc8-9711bd13f654"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("35feb245-2d59-242b-32f6-815a836df391"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("f87a5605-7ba9-410f-bf3e-aa1811b1265f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("38efae95-649a-9ed9-d691-d616f2ce8d59"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("f87a5605-7ba9-410f-bf3e-aa1811b1265f"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4f167e5c-48d6-c41e-5241-eed9f85fd830"),
           ModuleName="SystemAsset",
           ResourceId=new Guid("f87a5605-7ba9-410f-bf3e-aa1811b1265f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

});
            return resourceList.ToArray();
        }
    }
}
