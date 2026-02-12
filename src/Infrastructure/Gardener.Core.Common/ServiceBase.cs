// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using MiniExcelLibs;
using Gardener.Core.FileStore;
using Furion.DatabaseAccessor;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Furion.FriendlyException;
using Gardener.Core.Util;
using Gardener.Core.Util.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using Gardener.Core.Dtos.Constraints;
using Furion.DatabaseAccessor.Extensions;

namespace Gardener.Core.Common
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CU、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public abstract class ServiceBaseNoKey<TEntity, TEntityDto, TDbContextLocator> : IServiceBaseNoKey<TEntityDto> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new() where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// TEntity Repository
        /// </summary>
        public readonly IRepository<TEntity, TDbContextLocator> _repository;
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBaseNoKey(IRepository<TEntity, TDbContextLocator> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBaseNoKey(IRepository<TEntity> repository)
        {
            _repository = (IRepository<TEntity, TDbContextLocator>)repository;
        }

        /// <summary>
        /// 获取可读仓库对象
        /// </summary>
        /// <remarks>
        /// 获取可读仓库对象
        /// </remarks>
        /// <returns></returns>
        [NonAction]
        public virtual IPrivateReadableRepository<TEntity> GetReadableRepository()
        {
            return _repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Insert(TEntityDto input)
        {
            TEntity entity = input.Adapt<TEntity>();
            var newEntity = await _repository.InsertNowAsync(entity);
            TEntityDto result = newEntity.Entity.Adapt<TEntityDto>();
            //发送通知
            await EntityEventNotityUtil.NotifyInsertAsync(result);
            return result;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 批量添加数据
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<bool> BatchInsert(IEnumerable<TEntityDto> input)
        {
            if (!input.Any())
            {
                return false;
            }
            await _repository.InsertAsync(input.Select(x => x.Adapt<TEntity>()));
            await _repository.SaveNowAsync();
            foreach (var item in input)
            {
                await EntityEventNotityUtil.NotifyInsertAsync(item);
            }
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<bool> Update(TEntityDto input)
        {
            EntityEntry<TEntity> entityEntry = await _repository.UpdateNowAsync(input.Adapt<TEntity>());
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entityEntry.Entity.Adapt<TEntityDto>());
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 获取全部数据
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> GetAll()
        {
            var persons = GetReadableRepository().AsQueryable().Select(x => x.Adapt<TEntityDto>());
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="includLocked">是否包含锁定的</param>
        /// <remarks>
        /// 获取所有可用数据，(实现<see cref="IModelDeleted"/> <see cref="IModelLocked"/>时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> GetAllUsable([FromQuery] Guid? tenantId = null, [FromQuery] bool includLocked = false)
        {
            var paramList = new List<object>();
            StringBuilder where = new();
            where.Append(" 1==1 ");
            //判断是否有IsDelete
            Type type = typeof(TEntity);
            if (type.IsAssignableTo(typeof(IModelDeleted)))
            {
                where.Append($" &&  {nameof(IModelDeleted.IsDeleted)} == @{paramList.Count}");
                paramList.Add(false);
            }
            //判断是否有IsLock
            if (!includLocked && type.IsAssignableTo(typeof(IModelLocked)))
            {
                where.Append($" &&  {nameof(IModelLocked.IsLocked)} == @{paramList.Count}");
                paramList.Add(false);
            }
            //租户
            if (type.IsAssignableTo(typeof(IModelTenantId)) && tenantId != null)
            {
                where.Append($" &&  {nameof(IModelTenantId.TenantId)}.Equals(@{paramList.Count})");
                paramList.Add(tenantId);
            }
            var persons = GetReadableRepository().AsQueryable().Where(where.ToString(), paramList.ToArray());
            return await persons.Select(x => x.Adapt<TEntityDto>()).ToListAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据分页参数，分页获取数据
        /// </remarks>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<PageList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var queryable = GetReadableRepository().AsQueryable();

            var result = await queryable.ToPageAsync(pageIndex, pageSize);

            return result.Adapt<PageList<TEntityDto>>();
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
        public virtual async Task<PageList<TEntityDto>> Search(PageRequest request)
        {
            IQueryable<TEntity> queryable = GetSearchQueryable(request.FilterGroups);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<TEntityDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// <para>判断是否存在，根据输入条件组合进行数据查询判断是否存在。</para>
        /// <para>如果是支持多租户的对象，租户仅检测自己租户下的数据，如果需要检测全局，需要重新默认方法，使用非租户数据库上下文进行查询</para> 
        /// </remarks>
        /// <param name="filterGroups"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual Task<bool> Exists(List<FilterGroup> filterGroups)
        {
            IQueryable<TEntity> queryable = GetSearchQueryable(filterGroups, true);
            return queryable.AnyAsync();
        }

        /// <summary>
        /// 获取搜索数据Queryable方便自己重载定制
        /// </summary>
        /// <param name="filterGroups"></param>
        /// <param name="includeFakeDeleted">是否包含逻辑删除数据</param>
        /// <returns></returns>
        [NonAction]
        public virtual IQueryable<TEntity> GetSearchQueryable(List<FilterGroup> filterGroups, bool includeFakeDeleted = false)
        {
            return GetSearchQueryable(GetReadableRepository(), filterGroups, includeFakeDeleted);
        }

        /// <summary>
        /// 获取搜索数据Queryable方便自己重载定制
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="filterGroups"></param>
        /// <param name="includeFakeDeleted">是否包含逻辑删除数据</param>
        /// <returns></returns>
        [NonAction]
        public virtual IQueryable<TEntity> GetSearchQueryable(IPrivateReadableRepository<TEntity> repository, List<FilterGroup> filterGroups, bool includeFakeDeleted = false)
        {
            if (!includeFakeDeleted && typeof(TEntity).IsAssignableTo(typeof(IModelDeleted)))
            {
                FilterGroup defaultFilterGroup = new();
                defaultFilterGroup.AddRule(new FilterRule(nameof(IModelDeleted.IsDeleted), false, FilterOperate.Equal));
                filterGroups.Add(defaultFilterGroup);
            }
            Expression<Func<TEntity, bool>> expression = FilterHelper.GetExpression<TEntity>(filterGroups);
            IQueryable<TEntity> queryable = repository.AsQueryable(false).Where(expression);
            return queryable;
        }

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public virtual async Task<string> GenerateSeedData(PageRequest request)
        {
            IQueryable<TEntity> queryable = GetSearchQueryable(request.FilterGroups);
            PageList<TEntity> result = await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .ToPageAsync(request.PageIndex, request.PageSize);
            string[] excludeFields = { };
            if (typeof(TEntityDto).IsAssignableTo(typeof(IModelUpdated)))
            {
                excludeFields = [nameof(IModelUpdated.UpdateBy), nameof(IModelUpdated.UpdateIdentityType), nameof(IModelUpdated.UpdatedTime)];
            }
            return SeedDataGenerateHelper.Generate(result.Items, typeof(TEntityDto).Name, excludeFields: excludeFields);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <remarks>
        /// 根据搜索条件组合导出数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<string> Export(PageRequest request)
        {
            IQueryable<TEntity> queryable = GetSearchQueryable(request.FilterGroups);
            var list = await queryable
                 .OrderConditions(request.OrderConditions.ToArray())
                 .Select(x => x.Adapt<TEntityDto>()).ToListAsync();
            return await SaveToExportFile(list);
        }

        /// <summary>
        /// 保存为导出文件
        /// </summary>
        /// <param name="entities">数据列表</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [NonAction]
        public virtual async Task<string> SaveToExportFile(IEnumerable<TEntityDto> entities, string? fileName = null)
        {
            var memoryStream = new MemoryStream();
            memoryStream.SaveAs(entities);
            memoryStream.Seek(0, SeekOrigin.Begin);
            if (fileName == null)
            {
                fileName = typeof(TEntityDto).GetDescription() + DateTimeOffset.UtcNow.ToLocalTime().ToString("yyyyMMddHHmmssfff") + ".xlsx";
            }
            var fileService = App.GetRequiredService<IFileStoreService>();

            return await fileService.Save(memoryStream, "export/" + fileName);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sendEntityEventNotity"></param>
        /// <remarks>
        /// 根据模块名称删除,必须实现<see cref="IModelModule"/>
        /// </remarks>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<bool> DeleteByModuleName(string moduleName, bool sendEntityEventNotity = true)
        {
            if (!typeof(TEntity).IsAssignableTo(typeof(IModelModule)))
            {
                return false;
            }
            List<TEntityDto> entities = new List<TEntityDto>();
            foreach (var item in _repository.AsQueryable(false).Where(x => moduleName.Equals(((IModelModule)x).ModuleName)))
            {
                _repository.Delete(item);
                entities.Add(item.Adapt<TEntityDto>());
            }
            await _repository.SaveNowAsync();
            if (sendEntityEventNotity)
            {
                await EntityEventNotityUtil.NotifyDeletesAsync<TEntityDto>(entities);
            }
            return true;
        }

    }
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CU、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class ServiceBaseNoKey<TEntity, TEntityDto> : ServiceBaseNoKey<TEntity, TEntityDto, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBaseNoKey(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBaseNoKey(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto, TKey, TDbContextLocator> : ServiceBaseNoKey<TEntity, TEntityDto, TDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new() where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity, TDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
        /// <summary>
        /// 删除树型结构数据
        /// </summary>
        /// <remarks>
        /// <pre>在启用外键情况下，针对树形结构删除，先删除子级，再删除父级。</pre>
        /// <pre>如果某条数据删除失败，将中断，但不会回滚</pre>
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="getKey"></param>
        /// <returns></returns>
        [NonAction]
        protected async Task TreeDataDelete(TKey id, Expression<Func<TEntity, object?>> parentId, Func<TEntity, TKey> getKey)
        {
            string parentIdName = ExpressionHelper.GetFieldName(parentId);
            List<TEntity> children = await _repository.AsQueryable(false)
                .Where($"{parentIdName} != null && {parentIdName}.Equals({id})").ToListAsync();
            if (children.Any())
            {
                foreach (var item in children)
                {
                    await TreeDataDelete(getKey(item), parentId, getKey);
                }
            }
            if (await _repository.FindOrDefaultAsync(id)!=null)
            {
                await _repository.DeleteNowAsync(id);
                //发送删除通知
                await EntityEventNotityUtil.NotifyDeleteAsync<TEntityDto, TKey>(id);
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TKey id)
        {
            await _repository.DeleteNowAsync(id);
            //发送删除通知
            await EntityEventNotityUtil.NotifyDeleteAsync<TEntityDto, TKey>(id);
            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
        public virtual async Task<bool> Deletes([FromBody] TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                _repository.Delete(id);
            }
            await _repository.SaveNowAsync();
            await EntityEventNotityUtil.NotifyDeletesAsync<TEntityDto, TKey>(ids);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<bool> FakeDelete(TKey id)
        {
            await _repository.FakeDeleteNowByKeyAsync(id);
            await EntityEventNotityUtil.NotifyFakeDeleteAsync<TEntityDto, TKey>(id);
            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量逻辑删除")]
        public virtual async Task<bool> FakeDeletes([FromBody] TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.FakeDeleteByKeyAsync(id);
            }
            await _repository.SaveNowAsync();
            await EntityEventNotityUtil.NotifyFakeDeletesAsync<TEntityDto, TKey>(ids);
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，抛出异常,code=<see cref="ExceptionCode.Data_Not_Find"/>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Get(TKey id)
        {
            TEntity? entity = await GetReadableRepository().FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            TEntityDto result = entity.Adapt<TEntityDto>();
            return result;
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
        public virtual async Task<TEntityDto?> Find(TKey id)
        {
            TEntity? entity = await GetReadableRepository().FindOrDefaultAsync(id);
            return entity?.Adapt<TEntityDto>();
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须实现<see cref="IModelLocked"/>才能生效）
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<bool> Lock([ApiSeat(ApiSeats.ActionStart)] TKey id, bool isLocked = true)
        {
            var entity = await _repository.FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            if (entity is IModelLocked temp)
            {
                List<string> includeFields = new List<string> { nameof(IModelLocked.IsLocked) };
                temp.IsLocked = isLocked;
                await _repository.UpdateIncludeNowAsync(entity, includeFields);
                await EntityEventNotityUtil.NotifyLockAsync<TEntityDto>(entity.Adapt<TEntityDto>());
                return true;
            }
            else
            {
                throw Oops.BahLocalFrom<SharedLocalResource>($"{typeof(TEntity).Name} no implement {nameof(IModelLocked)}");
            }
        }

    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public abstract class ServiceBase<TEntity> : ServiceBase<TEntity, TEntity, int, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto> : ServiceBase<TEntity, TEntityDto, int, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto, TKey> : ServiceBase<TEntity, TEntityDto, TKey, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
