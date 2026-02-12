// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Dict.Services;
using TTShang.Core.EntityFramwork.Event;
using TTShang.Core.Enums;
using TTShang.Core.EventBus;
using TTShang.Core.UserCenter.Dtos;
using TTShang.Weighbridge.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TTShang.Weighbridge.Impl.Subscribes
{
    /// <summary>
    /// 租户事件订阅
    /// </summary>
    public class TenantEventSubscriber : IEventSubscriber
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TenantEventSubscriber> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopeFactory"></param>
        /// <param name="logger"></param>
        public TenantEventSubscriber(IServiceScopeFactory scopeFactory, ILogger<TenantEventSubscriber> logger)
        {
            _scopeFactory = scopeFactory;
            this.logger = logger;
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

            ICodeTypeService codeTypeService = scope.ServiceProvider.GetRequiredService<ICodeTypeService>();

            var codes = await codeTypeService.GetCodesByValue("number_of_axles");
            List<VehicleTypeDto> types = new List<VehicleTypeDto>();
            foreach (var code in codes)
            {
                int.TryParse(code.CodeValue, out int axles);
                double? maximumLoad = null;
                try
                {
                    if (code.ExtendParams != null)
                    {
                        JsonDocument document = JsonDocument.Parse(code.ExtendParams);
                        maximumLoad = document.RootElement.GetProperty("maximumLoad").GetDouble();
                    }
                }
                catch (Exception ex) 
                {
                    logger.LogError(ex, $"code {code.Id} get maximumLoad error");
                }
                types.Add(new VehicleTypeDto()
                {
                    Name = code.CodeName,
                    Axles = axles,
                    MaximumLoad = maximumLoad,
                    TenantId = tenant.Id
                });
            }
            if (types.Any())
            {
                IVehicleTypeService vehicleTypeService = scope.ServiceProvider.GetRequiredService<IVehicleTypeService>();
                await vehicleTypeService.BatchInsert(types);
            }
        }
    }
}
