// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;
using TTShang.Core.SystemAsset.Dtos;

namespace TTShang.Core.CodeGeneration.Impl
{
    /// <summary>
    /// 模块
    /// </summary>
    public class CodeGenerationServerModule : CodeGenerationModule, IServerModule
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
                    Hide = false,
                    Icon = "code-sandbox",
                    Id = new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
                    IsDeleted = false,
                    IsLocked = false,
                    Key = "dev_tools_code_gen",
                    ModuleName = "CodeGeneration",
                    Name = "代码生成",
                    Order = 41,
                    ParentId = new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
                    Path = "/dev_tools/code_gen",
                    ResourceFunctions = new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("1cc9e783-c562-c17b-a290-1981b5853e4d"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("2d940c63-cf62-4952-58ae-619a1a8b2cdf"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("346a0da6-143c-6881-c920-baf7199014de"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("399b7669-581e-8e04-36c3-047211b152b8"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4001ba8b-3c1d-5dfa-f80f-08d70f0614ca"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("40c26ba7-568b-8c5f-80cc-41e14df77072"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("4bda28dd-7384-3974-d481-9399e93e4081"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("508627c0-9d7b-a98a-34dd-cbd2ff9485bc"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("53e1d480-cf2b-f06b-38b3-3fb185e6ff60"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("5c59ff62-d650-55bb-919e-d49dfade9ac6"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("602a544c-471c-ac57-9d36-c672f1bfa7e9"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("60592a74-2ba9-3bbd-79fc-9b6e2fdb169e"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("607dd7ee-3a14-730b-1847-fd6d32062e0d"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("64a7bc30-2d46-a05d-2c71-0859e215bdd2"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("676d3a1f-15f5-1047-867f-a7f6927a29e9"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("707c64b1-ec6a-3b5d-ea83-fa3543faa43a"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7c5b065f-aeed-4d7f-76e3-511d4d2d00b0"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("7da01d3d-bdd0-8eee-bb15-15c31bedb5fa"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("973e329a-a915-22f9-aab3-b542c0cb5efc"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("98923c82-9917-8463-6acd-324e1d143970"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9b0c476e-c82f-55fc-1b40-806094fb929f"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("9c4626c8-35eb-c414-75f3-95d5fb6ae119"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b2584ad7-c17f-4684-14a4-fcab7fc1efc3"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b4e66a15-b944-d503-15c3-551dbbbfd4a9"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("b8985a75-541c-01f5-4a5d-95594f513c33"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("be0601ce-4063-8b5e-8677-442ce9cd6fb1"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c41e3260-259b-a4a6-e6b3-abb83b8f5e8f"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("c9378b90-2eb9-2089-a64d-af372e49e4d7"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("d2af113e-df6d-e37b-677e-a582833ef1d1"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("db1de679-1a2d-c528-23ad-12d35f0254fa"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e64a2710-66c1-0802-e569-20eed4535b8d"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e7a4f1f2-f7f9-c74e-429d-bd04940e9e70"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("e92765ba-021f-6827-ec6e-85d744390b91"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("eb5c91bb-5b9c-7a3e-b78e-777b3700b83c"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fb179c59-ce58-5ecf-e397-11f03619289f"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }
,
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("fba54542-01e3-a2bc-2870-4b2316e67fed"),
           ModuleName="CodeGeneration",
           ResourceId=new Guid("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),
        }

    },
                    SupportMultiTenant = false,
                    Type = ResourceType.Menu,
                }
            };
        }
    }
}
