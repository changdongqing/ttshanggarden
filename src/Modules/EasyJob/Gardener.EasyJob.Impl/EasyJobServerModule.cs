// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Dtos;
using Gardener.Core.Module;
using Gardener.Core.SystemAsset.Dtos;

namespace Gardener.EasyJob.Impl
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EasyJobServerModule : EasyJobModule, IServerModule
    {
        /// <summary>
        /// 注册字典
        /// </summary>
        /// <returns></returns>
        public CodeTypeDto[]? RegisterDic()
        {
            return new CodeTypeDto[] 
            {

                new CodeTypeDto() {
                    CodeTypeName="定时任务统计查询日期",
                    CodeTypeValue="easy_job_count_query_date",
                    IsLocked=false,
                    IsDeleted=false,
                    CreateBy="1",
                    CreateIdentityType=Enum.Parse<IdentityType>("User"),
                    Codes=new CodeDto[]
                    {
                        new CodeDto() {CodeValue="0",CodeName="Today",Order=0,Id=12,IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User")},
                        new CodeDto() {CodeValue="7",CodeName="A week",Order=5,Id=13,IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User")},
                        new CodeDto() {CodeValue = "30", CodeName = "A month", Order = 10, Id = 14, IsLocked = false, IsDeleted = false, CreateBy = "1", CreateIdentityType = Enum.Parse < IdentityType >("User")},
                        new CodeDto() {CodeValue = "180", CodeName = "Half a year", Order = 15, Id = 15, IsLocked = false, IsDeleted = false, CreateBy = "1", CreateIdentityType = Enum.Parse < IdentityType >("User")},
                        new CodeDto() {CodeValue = "365", CodeName = "A year", Order = 20, Id = 16, IsLocked = false, IsDeleted = false, CreateBy = "1", CreateIdentityType = Enum.Parse < IdentityType >("User")},
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
       Icon="hourglass",
       Id=new Guid("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job",
       ModuleName="EasyJob",
       Name="定时任务",
       Order=100,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_dashboard",
       ModuleName="EasyJob",
       Name="仪表盘",
       Order=5,
       ParentId=new Guid("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),
       Path="/system_manager/easy_job_dashboard",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0df540b7-2aad-7faf-4302-3bf96bcc8998"),
           ModuleName="EasyJob",
           ResourceId=new Guid("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2da3b526-804a-1d6d-bb17-be140245d3b1"),
           ModuleName="EasyJob",
           ResourceId=new Guid("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("542a317a-c9a3-b77e-c530-402b90468e9f"),
           ModuleName="EasyJob",
           ResourceId=new Guid("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7846df31-7fb6-9b6f-0912-4a5a8232ff8d"),
           ModuleName="EasyJob",
           ResourceId=new Guid("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail",
       ModuleName="EasyJob",
       Name="任务",
       Order=10,
       ParentId=new Guid("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),
       Path="/system_manager/easy_job_detail",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d137d256-a643-4e1d-bec2-2489f4f3630c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_add",
       ModuleName="EasyJob",
       Name="添加",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e044ee75-6d74-0997-fbce-cd15fbc84cb7"),
           ModuleName="EasyJob",
           ResourceId=new Guid("d137d256-a643-4e1d-bec2-2489f4f3630c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("e787f680-8ad9-4154-a036-4978162c8b56"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_delete",
       ModuleName="EasyJob",
       Name="删除",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("02f36096-0f93-afdd-8045-c1596dc60a26"),
           ModuleName="EasyJob",
           ResourceId=new Guid("e787f680-8ad9-4154-a036-4978162c8b56"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("f38347fd-11a3-4e1c-a1b0-a445510e7d8c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_delete_selected",
       ModuleName="EasyJob",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("ae604973-bb28-4deb-87a5-c3da8b88d6d3"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_detail",
       ModuleName="EasyJob",
       Name="查看详情",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b37afa89-f7c6-bcea-b0a3-d433cafc5752"),
           ModuleName="EasyJob",
           ResourceId=new Guid("ae604973-bb28-4deb-87a5-c3da8b88d6d3"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("696c06fd-a230-4472-adca-d378747091a4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_edit",
       ModuleName="EasyJob",
       Name="编辑",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e62e056a-06bf-56e5-7213-76aad3494a84"),
           ModuleName="EasyJob",
           ResourceId=new Guid("696c06fd-a230-4472-adca-d378747091a4"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("df23010b-e960-4c50-b114-e84df2edda4f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_log",
       ModuleName="EasyJob",
       Name="日志",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_monitor_log",
       ModuleName="EasyJob",
       Name="实时监控日志",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("04f1ece6-5029-dc20-413e-7f3d510ee36a"),
           ModuleName="EasyJob",
           ResourceId=new Guid("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0dec5a5d-cfd4-132e-c670-5063a760567f"),
           ModuleName="EasyJob",
           ResourceId=new Guid("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1c3c7507-2831-7abc-e4a3-e50c5545aaef"),
           ModuleName="EasyJob",
           ResourceId=new Guid("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("5e858248-f765-4412-9753-92621f20f611"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_refresh",
       ModuleName="EasyJob",
       Name="刷新",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("765e63a2-8805-a06a-ca47-25cb898d26dd"),
           ModuleName="EasyJob",
           ResourceId=new Guid("5e858248-f765-4412-9753-92621f20f611"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("54e062f1-d353-4a67-905e-f2cc5f14d689"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_run",
       ModuleName="EasyJob",
       Name="运行",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fe961bad-ceb4-bbae-1df2-bb5ef987acf8"),
           ModuleName="EasyJob",
           ResourceId=new Guid("54e062f1-d353-4a67-905e-f2cc5f14d689"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("693cd650-3b03-4bdf-8080-14112547329c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_detail_search",
       ModuleName="EasyJob",
       Name="搜索",
       Order=0,
       ParentId=new Guid("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("765e63a2-8805-a06a-ca47-25cb898d26dd"),
           ModuleName="EasyJob",
           ResourceId=new Guid("693cd650-3b03-4bdf-8080-14112547329c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9cda067c-8177-41ce-be76-25230ecb59a4"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_log",
       ModuleName="EasyJob",
       Name="日志",
       Order=90,
       ParentId=new Guid("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),
       Path="/system_manager/easy_job_log",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3e23e69d-8e27-466b-bfc6-a8f1f191549d"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_log_detail",
       ModuleName="EasyJob",
       Name="详情",
       Order=0,
       ParentId=new Guid("9cda067c-8177-41ce-be76-25230ecb59a4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5bed516a-043c-c186-d662-c456938f4a0c"),
           ModuleName="EasyJob",
           ResourceId=new Guid("3e23e69d-8e27-466b-bfc6-a8f1f191549d"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("21b2aec3-0c17-4c3f-82f7-dfa0ab76877a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_log_refresh",
       ModuleName="EasyJob",
       Name="刷新",
       Order=0,
       ParentId=new Guid("9cda067c-8177-41ce-be76-25230ecb59a4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1c3c7507-2831-7abc-e4a3-e50c5545aaef"),
           ModuleName="EasyJob",
           ResourceId=new Guid("21b2aec3-0c17-4c3f-82f7-dfa0ab76877a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("a0a21d0c-b733-40e7-833f-73c97baf913a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_log_search",
       ModuleName="EasyJob",
       Name="搜索",
       Order=0,
       ParentId=new Guid("9cda067c-8177-41ce-be76-25230ecb59a4"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1c3c7507-2831-7abc-e4a3-e50c5545aaef"),
           ModuleName="EasyJob",
           ResourceId=new Guid("a0a21d0c-b733-40e7-833f-73c97baf913a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger",
       ModuleName="EasyJob",
       Name="触发器",
       Order=20,
       ParentId=new Guid("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),
       Path="/system_manager/easy_job_trigger",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("cb7772c4-6dda-4c0a-aa7b-c506b303da02"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_add",
       ModuleName="EasyJob",
       Name="添加",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("50859992-54d2-b285-7154-9c359f786ad8"),
           ModuleName="EasyJob",
           ResourceId=new Guid("cb7772c4-6dda-4c0a-aa7b-c506b303da02"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d25b3920-8833-4168-bff1-1065cd72c8c7"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_delete",
       ModuleName="EasyJob",
       Name="删除",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("10ee09e4-385c-5ccf-d39a-c8fd111320c6"),
           ModuleName="EasyJob",
           ResourceId=new Guid("d25b3920-8833-4168-bff1-1065cd72c8c7"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("aa2109af-a9cd-48fd-b8b4-a872749b14eb"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_delete_selected",
       ModuleName="EasyJob",
       Name="删除选中",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("9299ac14-8d67-45a0-846e-ab35d15c0fbc"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_detail",
       ModuleName="EasyJob",
       Name="查看详情",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9ce39ec5-f460-debb-3cb5-1f10a765850d"),
           ModuleName="EasyJob",
           ResourceId=new Guid("9299ac14-8d67-45a0-846e-ab35d15c0fbc"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("61f0721c-a1d2-4b11-99e0-2e56533a433c"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_edit",
       ModuleName="EasyJob",
       Name="编辑",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("875b5177-41f9-85ec-8e28-0d6a6810c474"),
           ModuleName="EasyJob",
           ResourceId=new Guid("61f0721c-a1d2-4b11-99e0-2e56533a433c"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("3bd11c81-982f-400a-b6e8-d9a27b8baee1"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_log",
       ModuleName="EasyJob",
       Name="日志",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1c3c7507-2831-7abc-e4a3-e50c5545aaef"),
           ModuleName="EasyJob",
           ResourceId=new Guid("3bd11c81-982f-400a-b6e8-d9a27b8baee1"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_monitor_log",
       ModuleName="EasyJob",
       Name="实时监控日志",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("04f1ece6-5029-dc20-413e-7f3d510ee36a"),
           ModuleName="EasyJob",
           ResourceId=new Guid("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0dec5a5d-cfd4-132e-c670-5063a760567f"),
           ModuleName="EasyJob",
           ResourceId=new Guid("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("bfa94510-818b-4058-b20f-e4c95ca23a5b"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_monitor_state",
       ModuleName="EasyJob",
       Name="实时监控状态",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("04f1ece6-5029-dc20-413e-7f3d510ee36a"),
           ModuleName="EasyJob",
           ResourceId=new Guid("bfa94510-818b-4058-b20f-e4c95ca23a5b"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("0dec5a5d-cfd4-132e-c670-5063a760567f"),
           ModuleName="EasyJob",
           ResourceId=new Guid("bfa94510-818b-4058-b20f-e4c95ca23a5b"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("01061a49-b2d6-4c14-887b-e23ae4539031"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_pause",
       ModuleName="EasyJob",
       Name="暂停",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1cd3e1fa-4133-cec0-7ed3-d366141e5306"),
           ModuleName="EasyJob",
           ResourceId=new Guid("01061a49-b2d6-4c14-887b-e23ae4539031"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("95d5c35c-fdb3-4fec-bc6c-92aa5f61680f"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_refresh",
       ModuleName="EasyJob",
       Name="刷新",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9a760bc8-1361-a493-a4be-cac2b5d43fa3"),
           ModuleName="EasyJob",
           ResourceId=new Guid("95d5c35c-fdb3-4fec-bc6c-92aa5f61680f"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("d1c1f5c6-907b-47db-9d1f-6c6d87a64494"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_search",
       ModuleName="EasyJob",
       Name="搜索",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9a760bc8-1361-a493-a4be-cac2b5d43fa3"),
           ModuleName="EasyJob",
           ResourceId=new Guid("d1c1f5c6-907b-47db-9d1f-6c6d87a64494"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("54dc1159-93cd-4690-9ec1-f45e9a5dca7a"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_easy_job_trigger_start",
       ModuleName="EasyJob",
       Name="开启",
       Order=0,
       ParentId=new Guid("8d1e102e-02f6-4735-a46e-918ded1a9a19"),
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5d98cec5-49f1-ea2e-8889-441d0ee5d53f"),
           ModuleName="EasyJob",
           ResourceId=new Guid("54dc1159-93cd-4690-9ec1-f45e9a5dca7a"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Action,
    }

};
        }
    }
}
