// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.LicensePlateRecognition.Services;
using Gardener.Core.LicensePlateRecognition;
using Gardener.Core.Module;

namespace Gardener.Core.Api.Impl.LicensePlateRecognition
{
    /// <summary>
    /// LPR
    /// </summary>
    public class LPRServerModule : LPRModule, IServerModule
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
                return [typeof(LicencePlateService)];
            }
        }
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
       Icon="picture",
       Id=new Guid("1c2e76dd-147c-4b45-bc91-26ab62f3967e"),
       IsDeleted=false,
       IsLocked=false,
       Key="dev_tools_lpr_test",
       ModuleName="LPR",
       Name="车牌识别",
       Order=50,
       ParentId=new Guid("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),
       Path="/lpr/test",
       ResourceFunctions=new[]{
            new ResourceFunctionDto()
        {
           FunctionId=new Guid("43f01a8b-5d19-67c0-1057-9df8d64d0b9c"),
           ModuleName="LPR",
           ResourceId=new Guid("1c2e76dd-147c-4b45-bc91-26ab62f3967e"),
        }

    },
       SupportMultiTenant=false,
       Type=ResourceType.Menu,
    }

};
        }
    }
}
