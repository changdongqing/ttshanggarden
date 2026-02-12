// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using Gardener.Core.Localization;
using Gardener.EasyJob.Impl.Entities;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-触发器服务
    /// </summary>
    [ApiDescriptionSettings("EasyJob", Module = "job")]
    public class SysJobTriggerService : ServiceBase<SysJobTrigger, SysJobTriggerDto, int>, ISysJobTriggerService, IDynamicApiController
    {
        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
        private readonly ISchedulerFactory _schedulerFactory;
        /// <summary>
        /// 系统作业任务触发器服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="schedulerFactory"></param>
        public SysJobTriggerService(IRepository<SysJobTrigger> repository, ISchedulerFactory schedulerFactory) : base(repository)
        {
            _sysJobTriggerRep = repository;
            _schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<SysJobTriggerDto> Insert(SysJobTriggerDto input)
        {
            var isExist = await _sysJobTriggerRep.AnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
            if (isExist)
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetValue<EasyJobLocalResource>(nameof(SysJobTriggerDto.TriggerId)));
            //dto->entity
            var sysJobTrigger = input.Adapt<SysJobTrigger>();
            //entity->furion
            var jobTrigger = sysJobTrigger.Adapt<Trigger>();

            var scheduler = _schedulerFactory.GetJob(input.JobId);
            if (scheduler == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Scheduler_Not_Find);
            }
            scheduler.AddTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));
            return input;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(SysJobTriggerDto input)
        {
            var isExist = await _sysJobTriggerRep.AnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
            if (isExist)
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetValue<EasyJobLocalResource>(nameof(SysJobTriggerDto.TriggerId)));
            //dto->entity
            var sysJobTrigger = input.Adapt<SysJobTrigger>();
            //entity->furion
            var jobTrigger = sysJobTrigger.Adapt<Trigger>();

            var scheduler = _schedulerFactory.GetJob(input.JobId);
            if (scheduler == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Scheduler_Not_Find);
            }
            await _sysJobTriggerRep.UpdateNowAsync(sysJobTrigger);

            scheduler.UpdateTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));

            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            SysJobTrigger jobTrigger = await _sysJobTriggerRep.SingleOrDefaultAsync(x => x.Id == id, false);
            if (jobTrigger == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            scheduler?.RemoveTrigger(jobTrigger.TriggerId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下行代码确保触发器能被删除
            List<SysJobTrigger> jobTriggers = await _sysJobTriggerRep.AsQueryable(false).Where(u => u.JobId == jobTrigger.JobId && u.TriggerId == jobTrigger.TriggerId).ToListAsync();
            jobTriggers.ForEach(x =>
            {
                _sysJobTriggerRep.DeleteNow(x);
            });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> Deletes([FromBody] int[] ids)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> FakeDelete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> FakeDeletes([FromBody] int[] ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <remarks>
        /// 暂停该触发器
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> Pause([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobTrigger jobTrigger = _sysJobTriggerRep.SingleOrDefault(x => x.Id == id, false);
            if (jobTrigger == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            if (scheduler == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Scheduler_Not_Find);
            }
            Trigger? trigger;
            scheduler.TryGetTrigger(jobTrigger.TriggerId, out trigger);
            if (trigger == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            bool result= scheduler.PauseTrigger(jobTrigger.TriggerId);
            if(result)
            {
                jobTrigger.Status = Enums.TriggerStatus.Pause;
                _sysJobTriggerRep.UpdateIncludeNow(jobTrigger, new string[] { nameof(SysJobTrigger.Status) });
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 启动触发器
        /// </summary>
        /// <remarks>
        /// 启动该触发器
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> Start([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobTrigger jobTrigger = _sysJobTriggerRep.SingleOrDefault(x => x.Id == id, false);
            if (jobTrigger == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            if (scheduler == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Scheduler_Not_Find);
            }
            bool result = scheduler.StartTrigger(jobTrigger.TriggerId);
            if (result)
            {
                jobTrigger.Status = Enums.TriggerStatus.Ready;
                _sysJobTriggerRep.UpdateIncludeNow(jobTrigger, new string[] { nameof(SysJobTrigger.Status) });
            }
            return Task.FromResult(result);
        }
    }
}
