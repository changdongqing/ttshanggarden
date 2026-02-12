// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Gardener.Core.SystemAsset.Dtos;

namespace Gardener.WoChat.Impl
{
    /// <summary>
    /// 模块
    /// </summary>
    public class WoChatServerModule : WoChatModule, IServerModule
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
       Hide=true,
       Id=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
       IsDeleted=false,
       IsLocked=false,
       Key="global_wo_chat",
       ModuleName="WoChat",
       Name="WoChat聊天",
       Order=0,
       ParentId=new Guid("3c124d95-dd76-4903-b240-a4fe4df93868"),
       Path="/wo-chat",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("12ac2c49-ad3f-608e-385e-9598e08d1a81"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("34a5011a-8764-9b3a-9133-9041188063b5"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("39dfcd96-96e0-4fa6-d2b5-9e576312f470"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("501b7726-b971-66c6-ca1b-405296c0332c"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5890d62a-0061-8f70-fe54-a2a6692edeef"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5fccbd7e-2d07-6671-914c-8e61ccbc055b"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("a5cb2046-fad0-5b82-bd74-f7edba6bf5cf"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b974db10-43ef-7296-c8f7-c59c73d9ebe2"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("bf4d6a43-d586-9f7f-b002-02da9a66aee3"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c4abf22b-7bc9-e7cd-d5d2-1b771005c65c"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("cc38cabd-948d-b681-672b-cea4876dbf28"),
           ModuleName="WoChat",
           ResourceId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
        }

    },
       SupportMultiTenant=true,
       Type=ResourceType.Menu,
    }
,
    new ResourceDto()
    {
       Hide=false,
       Id=new Guid("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),
       IsDeleted=false,
       IsLocked=false,
       Key="global_wo_chat_btn",
       ModuleName="WoChat",
       Name="WoChat聊天按钮",
       Order=0,
       ParentId=new Guid("e20aa092-a80a-45a7-90b1-11e01b26cc98"),
       Remark="WoChat聊天按钮显资源",
       SupportMultiTenant=true,
       Type=ResourceType.Action,
    }

};
        }
    }
}
