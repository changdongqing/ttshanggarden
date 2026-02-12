// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Core.Module;
using Gardener.Core.SystemAsset.Dtos;

namespace Gardener.ToolBox.Impl
{
    /// <summary>
    /// 模块
    /// </summary>
    public class ToolBoxServerModule : ToolBoxModule, IServerModule
    {
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
       Icon="tool",
       Id=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools",
       ModuleName="ToolBox",
       Name="开发工具",
       Order=200,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="bg-colors",
       Id=new Guid("7440535c-8568-4d1a-be5c-b7a93cb9d282"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools_color",
       ModuleName="ToolBox",
       Name="颜色",
       Order=10,
       ParentId=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       Path="/tools/colors",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="iconfont icon-cron",
       Id=new Guid("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools_cron",
       ModuleName="ToolBox",
       Name="Cron",
       Order=20,
       ParentId=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       Path="/tools/cron",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("16ef70ab-811c-fbb3-a825-6d787870726a"),
           ModuleName="ToolBox",
           ResourceId=new Guid("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("3e1af506-1e51-5d44-4b9f-0a44bd8f1f49"),
           ModuleName="ToolBox",
           ResourceId=new Guid("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="iconfont icon-guid",
       Id=new Guid("cc41cdf3-8595-47b8-8c59-a171bf9b061a"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools_guid",
       ModuleName="ToolBox",
       Name="Guid",
       Order=30,
       ParentId=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       Path="/tools/guid",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="field-time",
       Id=new Guid("5e667965-2515-4410-a40b-370546f81cc5"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools_timestamp",
       ModuleName="ToolBox",
       Name="时间戳",
       Order=40,
       ParentId=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       Path="/tools/timestamp",
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }

};
        }
    }
}
