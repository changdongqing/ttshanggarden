// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DataValidation;
using Furion.FriendlyException;
using Gardener.Core.Authorization.Services;
using Gardener.Core.Common;
using Gardener.Core.Dtos;
using Gardener.Core.Enums;
using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Services;
using Gardener.Core.Resources;
using Gardener.Weighbridge.Enums;
using Gardener.Weighbridge.Impl.Entities;
using Gardener.Weighbridge.Resources;
using Gardener.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Net.Sockets;

namespace Gardener.Weighbridge.Impl.Services
{
    /// <summary>
    /// 称重记录服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class WeighingRecordService : ServiceBase<WeighingRecord, WeighingRecordDto, Guid, GardenerMultiTenantDbContextLocator>, IWeighingRecordService
    {
        private readonly IRepository<WeighingRecordLog, GardenerMultiTenantDbContextLocator> _weighingRecordLogRepository;
        private readonly IRepository<WeighbridgeConfig, GardenerMultiTenantDbContextLocator> _weighbridgeConfigLogRepository;

        private readonly ICommodityService _commodityService;
        private readonly IPrintTemplateService _printTemplateService;
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 称重记录服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="weighingRecordLogRepository"></param>
        /// <param name="commodityService"></param>
        /// <param name="printTemplateService"></param>
        /// <param name="identityService"></param>
        /// <param name="weighbridgeConfigLogRepository"></param>
        public WeighingRecordService(IRepository<WeighingRecord, GardenerMultiTenantDbContextLocator> repository, IRepository<WeighingRecordLog, GardenerMultiTenantDbContextLocator> weighingRecordLogRepository, ICommodityService commodityService, IPrintTemplateService printTemplateService, IIdentityService identityService, IRepository<WeighbridgeConfig, GardenerMultiTenantDbContextLocator> weighbridgeConfigLogRepository) : base(repository)
        {
            _weighingRecordLogRepository = weighingRecordLogRepository;
            _commodityService = commodityService;
            _printTemplateService = printTemplateService;
            _identityService = identityService;
            _weighbridgeConfigLogRepository = weighbridgeConfigLogRepository;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，抛出异常,code=<see cref="ExceptionCode.Data_Not_Find"/>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<WeighingRecordDto> Get(Guid id)
        {
            var weighingRecord = await base.Get(id);
            weighingRecord.WeighingRecordLogs = await _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(weighingRecord.Id)).Select(x => x.Adapt<WeighingRecordLogDto>()).ToListAsync();
            return weighingRecord;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，返回null
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("find/{id}")]
        public override async Task<WeighingRecordDto?> Find(Guid id)
        {
            var weighingRecord = await base.Find(id);
            if (weighingRecord != null)
            {
                weighingRecord.WeighingRecordLogs = await _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(weighingRecord.Id)).Select(x => x.Adapt<WeighingRecordLogDto>()).ToListAsync();
            }
            return weighingRecord;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<WeighingRecordDto>> Search(PageRequest request)
        {
            PageList<WeighingRecordDto> page = await base.Search(request);
            if (page.Items.Any())
            {
                var configMap = await _weighbridgeConfigLogRepository.AsQueryable(false).Where(x => page.Items.Select(y => y.WeighbridgeConfigId).Any(y => y == x.Id)).Select(x => x.Adapt<WeighbridgeConfigDto>()).ToDictionaryAsync(x => x.Id, x => x);
                List<WeighingRecordLogDto> logs = await _weighingRecordLogRepository.AsQueryable(false).Where(x => page.Items.Select(p => p.Id).Any(p => p.Equals(x.WeighingRecordId))).Select(x => x.Adapt<WeighingRecordLogDto>()).ToListAsync();
                foreach (var item in page.Items)
                {
                    item.WeighingRecordLogs = logs.Where(x => x.WeighingRecordId.Equals(item.Id)).OrderBy(x => x.CreatedTime).ToList();
                    item.WeighbridgeConfig = configMap.GetValueOrDefault(item.WeighbridgeConfigId);
                }
            }
            return page;
        }

        /// <summary>
        /// 根据车牌号获取最新记录
        /// </summary>
        /// <remarks>
        /// 根据车牌号获取最新称重记录，如果没有返回null
        /// </remarks>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="noLoadPriority">无货优先</param>
        /// <returns></returns>
        public async Task<WeighingRecordDto?> FindLastByPlateNumber(string plateNumber, bool noLoadPriority)
        {
            if (noLoadPriority)
            {
                var noLoadRecord = await _repository.AsQueryable(false).Where(x => x.PlateNumber.Equals(plateNumber) && x.TareWeight > 0 && x.IsDeleted == false && x.IsLocked == false).OrderByDescending(x => x.CreatedTime).Select(x => x.Adapt<WeighingRecordDto>()).FirstOrDefaultAsync();
                if (noLoadRecord != null)
                {
                    return noLoadRecord;
                }
            }
            return await _repository.AsQueryable(false).Where(x => x.PlateNumber.Equals(plateNumber) && x.IsDeleted == false && x.IsLocked == false).OrderByDescending(x => x.CreatedTime).Select(x => x.Adapt<WeighingRecordDto>()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 保存称重记录
        /// </summary>
        /// <remarks>
        /// 保存称重记录
        /// </remarks>
        /// <param name="weighingRecord"></param>
        /// <returns></returns>
        public async Task<WeighingRecordDto?> SaveWeighingRecord(WeighingRecordDto weighingRecord)
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null)
            {
                return null;
            }
            weighingRecord.OperatorName = identity.NickName ?? identity.Name ?? identity.Id;
            weighingRecord.WeighingNumber = 1;
            WeighingRecordLog log = new WeighingRecordLog()
            {
                PlateNumber = weighingRecord.PlateNumber,
                Driver = weighingRecord.Driver,
                VehicleType = weighingRecord.VehicleType,
                MaximumLoad = weighingRecord.MaximumLoad,
                WeighingStatus = weighingRecord.WeighingStatus,
                WeighbridgeConfigId = weighingRecord.WeighbridgeConfigId,
                OperatorName = weighingRecord.OperatorName,
                Weight = weighingRecord.Weight
            };

            #region 货物处理
            if (!string.IsNullOrEmpty(weighingRecord.CommodityName) && !string.IsNullOrEmpty(weighingRecord.CommodityCode))
            {
                var commodity = await _commodityService.FindByCode(weighingRecord.CommodityCode);
                if (commodity != null && !commodity.CommodityName.Equals(weighingRecord.CommodityName))
                {
                    //货名和货号不匹配
                    throw Oops.Bah(WeighbridgeLocalResource.CommodityNameNotMatchCode);
                }
                if (commodity == null)
                {
                    var commodity1 = await _commodityService.FindByName(weighingRecord.CommodityName);
                    if (commodity1 != null)
                    {
                        //货名和货号不匹配
                        throw Oops.Bah(WeighbridgeLocalResource.CommodityNameNotMatchCode);
                    }
                    //添加
                    await _commodityService.Insert(new CommodityDto()
                    {
                        CommodityCode = weighingRecord.CommodityCode,
                        CommodityName = weighingRecord.CommodityName,
                    });
                }
                log.CommodityName = weighingRecord.CommodityName;
                log.CommodityCode = weighingRecord.CommodityCode;
            }
            #endregion

            if (!Guid.Empty.Equals(weighingRecord.Id))
            {
                int count = await _weighingRecordLogRepository.AsQueryable(false).CountAsync(x => x.WeighingRecordId.Equals(weighingRecord.Id) && x.WeighingRecordId.Equals(weighingRecord.Id));
                weighingRecord.WeighingNumber = count + 1;

                WeighingRecord? oldRecord = await _repository.AsQueryable(false).Where(x => x.Id.Equals(weighingRecord.Id)).FirstOrDefaultAsync();
                if (oldRecord == null)
                {
                    throw Oops.Bah(SharedLocalResource.Item_Data_Not_Find, WeighbridgeLocalResource.WeighingRecord);
                }
                if (!string.IsNullOrEmpty(oldRecord.CommodityCode) && !string.IsNullOrEmpty(weighingRecord.CommodityCode))
                {
                    List<string> codes = oldRecord.CommodityCode.Split(",").ToList(); codes.Add(weighingRecord.CommodityCode);
                    weighingRecord.CommodityCode = string.Join(",", codes.Distinct());
                }
                if (!string.IsNullOrEmpty(oldRecord.CommodityName) && !string.IsNullOrEmpty(weighingRecord.CommodityName))
                {
                    List<string> names = oldRecord.CommodityName.Split(",").ToList(); names.Add(weighingRecord.CommodityName);
                    weighingRecord.CommodityName = string.Join(",", names.Distinct());
                }
                //上次的日志
                WeighingRecordLog? lastLog = await _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(weighingRecord.Id)).OrderByDescending(x => x.CreatedTime).FirstOrDefaultAsync();
                if (lastLog != null)
                {
                    if (WeighingStatus.LoadingGoods.Equals(lastLog.WeighingStatus) && WeighingStatus.LoadedGoods.Equals(weighingRecord.WeighingStatus))
                    {
                        weighingRecord.Weight = lastLog.Weight;
                        log.Weight = lastLog.Weight;
                    }
                    if (WeighingStatus.UnloadingGoods.Equals(lastLog.WeighingStatus) && WeighingStatus.UnloadedGoods.Equals(weighingRecord.WeighingStatus))
                    {
                        log.Weight = lastLog.Weight;
                    }
                    //日志中 重量=本次重量-上次重量
                    log.WeightChange = log.Weight - lastLog.Weight;
                }
                //卸货，不更新总重
                if (WeighingStatus.UnloadingGoods.Equals(weighingRecord.WeighingStatus) || WeighingStatus.UnloadedGoods.Equals(weighingRecord.WeighingStatus))
                {
                    weighingRecord.Weight = oldRecord.Weight;
                }
                //更新
                EntityEntry<WeighingRecord> weighingRecordEntiry = await _repository.UpdateIncludeNowAsync(weighingRecord.Adapt<WeighingRecord>(),
                    [
                        nameof(WeighingRecord.WeighbridgeConfigId),
                        nameof(WeighingRecord.Weight),
                        nameof(WeighingRecord.TareWeight),
                        nameof(WeighingRecord.Driver),
                        nameof(WeighingRecord.PlateNumber),
                        nameof(WeighingRecord.VehicleType),
                        nameof(WeighingRecord.MaximumLoad),
                        nameof(WeighingRecord.WeighingNumber),
                        nameof(WeighingRecord.WeighingStatus),
                        nameof(WeighingRecord.OperatorName),
                        nameof(WeighingRecord.CommodityName),
                        nameof(WeighingRecord.CommodityCode),
                        nameof(WeighingRecord.OperatorName)
                    ]);
            }
            else
            {
                //添加
                EntityEntry<WeighingRecord> weighingRecordEntiry = await _repository.InsertNowAsync(weighingRecord.Adapt<WeighingRecord>());
                weighingRecord = weighingRecordEntiry.Entity.Adapt<WeighingRecordDto>();
            }
            log.WeighingRecordId = weighingRecord.Id;
            await _weighingRecordLogRepository.InsertNowAsync(log);
            return weighingRecord;
        }

        /// <summary>
        /// 获取当天未结束记录
        /// </summary>
        /// <remarks>
        /// 获取当天未结束记录
        /// </remarks>
        /// <param name="weighbridgeConfigId"></param>
        /// <returns></returns>
        public Task<List<WeighingRecordDto>> GetTodayUnfinishedRecord(Guid weighbridgeConfigId)
        {
            DateTimeOffset beginTime = DateTimeOffset.Now.Date;
            DateTimeOffset endTime = beginTime.AddDays(1);
            return _repository.AsQueryable(false)
                .Where(x =>
                    x.WeighbridgeConfigId.Equals(weighbridgeConfigId) &&
                    x.IsDeleted == false &&
                    x.IsLocked == false &&
                    x.CreatedTime >= beginTime &&
                    x.CreatedTime < endTime &&
                    (x.WeighingStatus.Equals(WeighingStatus.NoLoadGoods) || x.WeighingStatus.Equals(WeighingStatus.LoadGoods))
                )
                .OrderByDescending(x => x.CreatedTime)
                .Select(x => x.Adapt<WeighingRecordDto>()).ToListAsync();
        }

        /// <summary>
        /// 获取记录打印数据base64格式
        /// </summary>
        /// <param name="weighingRecordId"></param>
        /// <param name="protocolType"></param>
        /// <param name="templateKey"></param>
        /// <remarks>
        /// 获取记录打印数据base64格式
        /// </remarks>
        /// <returns></returns>
        public async Task<string?> GetRecordPrintDataBase64(Guid weighingRecordId, [FromQuery] PrintProtocolType protocolType, [FromQuery] string templateKey)
        {
            var record = await base.Find(weighingRecordId);
            if (record == null)
            {
                return null;
            }
            List<WeighingRecordLogDto> logs = await _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(weighingRecordId) && x.IsDeleted == false && x.IsLocked == false).OrderBy(x => x.CreatedTime).Select(x => x.Adapt<WeighingRecordLogDto>()).ToListAsync();
            record.WeighingRecordLogs = logs;

            return await GenerateRecordPrintDataBase64(new GenerateRecordPrintDataInput()
            {
                ProtocolType = protocolType,
                TemplateKey = templateKey,
                WeighingRecord = record
            });
        }

        /// <summary>
        /// 将记录生成base64格式打印数据
        /// </summary>
        /// <remarks>
        /// 将记录生成base64格式打印数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string?> GenerateRecordPrintDataBase64(GenerateRecordPrintDataInput input)
        {
            return await _printTemplateService.ConvertToBase64(input.TemplateKey, input.ProtocolType, input.WeighingRecord, ["Gardener.Weighbridge", "Gardener.Core.Shared"], "Gardener.Weighbridge.Dtos", "Gardener.Weighbridge.Enums");
        }

        /// <summary>
        /// 获取称重记录日志列表
        /// </summary>
        /// <remarks>
        /// 根据称重记录编号，获取称重记录日志列表
        /// </remarks>
        /// <param name="weighingRecordId"></param>
        /// <returns></returns>
        public Task<List<WeighingRecordLogDto>> GetWeighingRecordLogs([ApiSeat(ApiSeats.ActionStart)] Guid weighingRecordId)
        {
            return _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(weighingRecordId)).OrderBy(x => x.CreatedTime).Select(x => x.Adapt<WeighingRecordLogDto>()).ToListAsync();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(WeighingRecordDto input)
        {
            if (input.WeighingRecordLogs.Any())
            {
                double lastWeight = 0;
                foreach (var item in input.WeighingRecordLogs.OrderBy(x=>x.CreatedTime))
                {

                    if (item.IsDeleted && !item.Id.Equals(Guid.Empty))
                    {
                        //需要删除
                        await _weighingRecordLogRepository.DeleteNowAsync(item.Id);
                    }
                    else if (item.Id.Equals(Guid.Empty))
                    {
                        //需要添加
                        item.TenantId = input.TenantId;
                        item.WeighbridgeConfigId = input.WeighbridgeConfigId;
                        item.WeighingRecordId = input.Id;
                        item.Driver = input.Driver;
                        item.PlateNumber = input.PlateNumber;
                        item.MaximumLoad = input.MaximumLoad;
                        item.OperatorName = input.OperatorName;
                        item.WeightChange = item.Weight - lastWeight;
                        await _weighingRecordLogRepository.InsertNowAsync(item.Adapt<WeighingRecordLog>());
                    }
                    else
                    {
                        item.TenantId = input.TenantId;
                        item.WeightChange = item.Weight - lastWeight;
                        await _weighingRecordLogRepository.UpdateIncludeNowAsync(item.Adapt<WeighingRecordLog>(), [
                            nameof(WeighingRecordLog.CommodityCode),
                            nameof(WeighingRecordLog.TenantId),
                            nameof(WeighingRecordLog.Weight),
                            nameof(WeighingRecordLog.WeightChange),
                            nameof(WeighingRecordLog.WeighingStatus),
                            nameof(WeighingRecordLog.CreatedTime),
                            nameof(WeighingRecordLog.TenantId),
                        nameof(WeighingRecordLog.CommodityName)]);
                    }
                    lastWeight = item.Weight;
                }
            }
            input.WeighingRecordLogs = [];
            input.WeighingNumber = await _weighingRecordLogRepository.AsQueryable(false).Where(x =>x.WeighingRecordId.Equals(input.Id) && x.IsDeleted == false).CountAsync();
            return await base.Update(input);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<WeighingRecordDto> Insert(WeighingRecordDto input)
        {

            List<WeighingRecordLogDto> weighingRecordLogs= input.WeighingRecordLogs.Where(x=>x.IsDeleted==false).OrderBy(x=>x.CreateBy).ToList();
            input.WeighingRecordLogs = [];
            input.WeighingNumber= weighingRecordLogs.Count;
            WeighingRecordDto weighingRecord=await base.Insert(input);

            if (weighingRecordLogs.Any())
            {
                double lastWeight = 0;
                foreach (var item in weighingRecordLogs.OrderBy(x => x.CreatedTime))
                {

                    //需要添加
                    item.TenantId = weighingRecord.TenantId;
                    item.WeighbridgeConfigId = weighingRecord.WeighbridgeConfigId;
                    item.WeighingRecordId = weighingRecord.Id;
                    item.Driver = weighingRecord.Driver;
                    item.PlateNumber = weighingRecord.PlateNumber;
                    item.MaximumLoad = weighingRecord.MaximumLoad;
                    item.OperatorName = weighingRecord.OperatorName;
                    item.WeightChange = item.Weight - lastWeight;
                    await _weighingRecordLogRepository.InsertNowAsync(item.Adapt<WeighingRecordLog>());
                    lastWeight = item.Weight;
                }
                weighingRecord.WeighingRecordLogs = await _weighingRecordLogRepository.AsQueryable(false).Where(x => x.WeighingRecordId.Equals(input.Id) && x.IsDeleted == false).OrderBy(x=>x.CreateBy).Select(x=>x.Adapt<WeighingRecordLogDto>()).ToListAsync();
            }
            return weighingRecord;
        }
    }
}
