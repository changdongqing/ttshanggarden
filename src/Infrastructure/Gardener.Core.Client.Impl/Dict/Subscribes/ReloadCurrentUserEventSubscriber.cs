// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Authorization.Events;
using Gardener.Core.Dict.Services;

namespace Gardener.Core.Client.Impl.Dict.Subscribes
{
    /// <summary>
    /// 重载用户后
    /// </summary>
    [ScopedService]
    public class ReloadCurrentUserEventSubscriber : EventSubscriberBase<ReloadCurrentUserEvent>
    {
        private readonly ICodeTypeService codeTypeService;
        /// <summary>
        /// 登录成功后
        /// </summary>
        /// <param name="codeTypeService"></param>
        public ReloadCurrentUserEventSubscriber(ICodeTypeService codeTypeService)
        {
            this.codeTypeService = codeTypeService;
        }

        public override async Task CallBack(ReloadCurrentUserEvent e)
        {
            DictHelper.InitAllCode(await codeTypeService.GetCodeDicByValues());
        }
    }
}
