// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.EntityFramwork.Event;
using TTShang.Core.EventBus;
using TTShang.Core.UserCenter.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.UserCenter.Subscribes
{
    /// <summary>
    /// 租户事件订阅
    /// </summary>
    public class TenantEventSubscriber : IEventSubscriber
    {
        private readonly IServiceScopeFactory _scopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopeFactory"></param>
        public TenantEventSubscriber(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe($"{nameof(EventGroup.EntityOperate)}Gardener.Core.UserCenter.Dtos.{nameof(SystemTenantDto)}{nameof(EntityOperateType.Insert)}")]
        public async Task InitTenantConfig(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            SystemTenantDto tenant = ((EntityChangeEvent<SystemTenantDto>)eventSource.Payload).Data;

            using var scope = _scopeFactory.CreateScope();

            ITenantConfigTemplateService tenantConfigTemplateService = scope.ServiceProvider.GetRequiredService<ITenantConfigTemplateService>();
            var templates = await tenantConfigTemplateService.GetAllUsable();
            if (templates != null && templates.Any())
            {
                List<SystemTenantConfigDto> systemTenantConfigs = new List<SystemTenantConfigDto>();
                foreach (var item in templates)
                {
                    SystemTenantConfigDto config = new SystemTenantConfigDto()
                    {
                        ConfigKey = item.ConfigKey,
                        ConfigValue = item.DefaultConfigValue,
                        Remark = item.Description,
                        TenantId = tenant.Id
                    };
                    systemTenantConfigs.Add(config);
                }
                ITenantConfigService tenantConfigService = scope.ServiceProvider.GetRequiredService<ITenantConfigService>();
                await tenantConfigService.BatchInsert(systemTenantConfigs);
            }


        }

        /// <summary>
        /// 变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.UserCenter.Dtos." + nameof(SystemTenantDto) + nameof(EntityOperateType.Delete))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.UserCenter.Dtos." + nameof(SystemTenantDto) + nameof(EntityOperateType.FakeDelete))]
        public async Task Delete(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            Guid id = eventSource.GetEventData<Guid>();
            using var scope = _scopeFactory.CreateScope();
            IRepository<SystemTenantConfig> repository = scope.ServiceProvider.GetRequiredService<IRepository<SystemTenantConfig>>();
            var configs = await repository.AsQueryable(false).Where(x => x.TenantId.Equals(id)).ToListAsync();
            if (configs != null && configs.Any())
            {
                await repository.DeleteNowAsync(configs);
            }
        }

        /// <summary>
        /// 变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.UserCenter.Dtos." + nameof(SystemTenantDto) + nameof(EntityOperateType.Deletes))]
        [EventSubscribe(nameof(EventGroup.EntityOperate) + "Gardener.Core.UserCenter.Dtos." + nameof(SystemTenantDto) + nameof(EntityOperateType.FakeDeletes))]
        public async Task Deletes(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            using var scope = _scopeFactory.CreateScope();
            IEnumerable<Guid> ids = eventSource.GetEventData<IEnumerable<Guid>>();
            IRepository<SystemTenantConfig> repository = scope.ServiceProvider.GetRequiredService<IRepository<SystemTenantConfig>>();
            var configs = await repository.AsQueryable(false).Where(x => ids.Contains(x.TenantId)).ToListAsync();
            if (configs != null && configs.Any())
            {
                await repository.DeleteNowAsync(configs);
            }
        }
    }
}
