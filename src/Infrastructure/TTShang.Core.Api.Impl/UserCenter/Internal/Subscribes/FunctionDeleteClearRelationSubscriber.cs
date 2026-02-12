// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Api.Impl.SystemAsset.Entities;
using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.EventBus;

namespace TTShang.Core.Api.Impl.UserCenter.Internal.Subscribes
{
    /// <summary>
    /// 功能点变化清除关联关系
    /// </summary>
    public class FunctionDeleteClearRelationSubscriber : IEventSubscriber, ISingleton
    {

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.Delete))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.FakeDelete))]
        public async Task Delete(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            IRepository<ResourceFunction> resourceFunctionRepository = Db.GetRepository<ResourceFunction>();
            IRepository<ClientFunction> clientFunctionRepository = Db.GetRepository<ClientFunction>();
            Guid id = eventSource.GetEventData<Guid>();
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.Deletes))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.FakeDeletes))]
        public async Task Deletes(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            IRepository<ResourceFunction> resourceFunctionRepository = Db.GetRepository<ResourceFunction>();
            IRepository<ClientFunction> clientFunctionRepository = Db.GetRepository<ClientFunction>();
            IEnumerable<Guid> ids = eventSource.GetEventData<IEnumerable<Guid>>();
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
        }

    }
}
