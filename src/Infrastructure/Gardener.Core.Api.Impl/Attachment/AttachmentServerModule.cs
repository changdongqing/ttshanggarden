// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Attachment.Services;
using Gardener.Core.Attachment;
using Gardener.Core.Module;

namespace Gardener.Core.Api.Impl.Attachment
{
    /// <summary>
    /// 附件模块-服务端
    /// </summary>
    internal class AttachmentServerModule : AttachmentModule, IServerModule
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
                return [typeof(AttachmentService)];
            }
        }

        /// <summary>
        /// 自动注册接口
        /// </summary>
        public bool AutoRegisterFunction => true;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResourceDto[]? RegisterResource()
        {
            return new[]{
    new ResourceDto()
    {
       Hide=false,
       Icon="file",
       Id=new Guid("925c3162-155c-4644-8ca2-075f9fc76235"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_attachment",
       ModuleName="Attachment",
       Name="附件管理",
       Order=50,
       ParentId=new Guid("c2090656-8a05-4e67-b7ea-62f178639620"),
       Path="/system_manager/attachment",
       Remark="附件管理",
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="",
       Id=new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_attachment_delete",
       ModuleName="Attachment",
       Name="删除附件",
       Order=0,
       ParentId=new Guid("925c3162-155c-4644-8ca2-075f9fc76235"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7453049c-9e15-d4e5-e6e2-a37b758faeb3"),
           ModuleName="Attachment",
           ResourceId=new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("896014d4-f095-2a89-16c9-7630babf3dd0"),
           ModuleName="Attachment",
           ResourceId=new Guid("f02f906a-7579-478a-9406-3c8fd2c54886"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Icon="null",
       Id=new Guid("d998802f-776e-4137-bc63-d8d818464f98"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_attachment_delete_selected",
       ModuleName="Attachment",
       Name="删除选中附件",
       Order=0,
       ParentId=new Guid("925c3162-155c-4644-8ca2-075f9fc76235"),
       Path="null",
       Remark="删除选中附件",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("61c0057f-d1be-cda7-a390-37f8d7b3ebc2"),
           ModuleName="Attachment",
           ResourceId=new Guid("d998802f-776e-4137-bc63-d8d818464f98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("99ff6717-b2f0-06ce-8dab-739064a55da6"),
           ModuleName="Attachment",
           ResourceId=new Guid("d998802f-776e-4137-bc63-d8d818464f98"),
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
       Id=new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_attachment_detail",
       ModuleName="Attachment",
       Name="查看附件",
       Order=0,
       ParentId=new Guid("925c3162-155c-4644-8ca2-075f9fc76235"),
       Path="",
       Remark="查看附件",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("00c9bde4-edab-a7a6-2e4a-1b7e2ed02330"),
           ModuleName="Attachment",
           ResourceId=new Guid("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),
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
       Id=new Guid("f1649263-ef9a-4f42-85ac-16009283efff"),
       IsDeleted=false,
       IsLocked=false,
       Key="system_manager_attachment_refresh",
       ModuleName="Attachment",
       Name="刷新附件",
       Order=0,
       ParentId=new Guid("925c3162-155c-4644-8ca2-075f9fc76235"),
       Path="",
       Remark="",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("70f85a66-4fea-48ee-51a9-520102a9d490"),
           ModuleName="Attachment",
           ResourceId=new Guid("f1649263-ef9a-4f42-85ac-16009283efff"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
