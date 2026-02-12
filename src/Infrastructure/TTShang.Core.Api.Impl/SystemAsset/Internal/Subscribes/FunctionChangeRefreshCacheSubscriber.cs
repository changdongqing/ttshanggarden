// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Api.Impl.SystemAsset.Entities;
using TTShang.Core.Cache;
using TTShang.Core.EventBus;

namespace TTShang.Core.Api.Impl.SystemAsset.Internal.Subscribes
{
    /// <summary>
    /// 功能点变化刷新接口点缓存
    /// </summary>
    internal class FunctionChangeRefreshCacheSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ICache cache;
        private readonly ApiQueryService apiSettingsQueryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="apiEndpointQueryService"></param>
        public FunctionChangeRefreshCacheSubscriber(ICache cache, ApiQueryService apiEndpointQueryService)
        {
            this.cache = cache;
            apiSettingsQueryService = apiEndpointQueryService;
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.Delete))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.FakeDelete))]
        public async Task Delete(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            Guid id = eventSource.GetEventData<Guid>();
            await apiSettingsQueryService.ClearCache(id);
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
            IEnumerable<Guid> ids = eventSource.GetEventData<IEnumerable<Guid>>();
            foreach (Guid id in ids)
            {
                await apiSettingsQueryService.ClearCache(id);
            }
        }
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.DeletesEntity))]
        public async Task DeletesEntity(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            IEnumerable<FunctionDto> functions = eventSource.GetEventData<IEnumerable<FunctionDto>>();
            foreach (var function in functions)
            {
                await apiSettingsQueryService.ClearCache(function.Id);
            }
        }
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.Update))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.SystemAsset.Dtos." + nameof(FunctionDto) + nameof(EntityOperateType.Lock))]
        public async Task Update(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            Function function = eventSource.GetEventData<Function>();
            await apiSettingsQueryService.ClearCache(function.Id);
        }
    }
}
