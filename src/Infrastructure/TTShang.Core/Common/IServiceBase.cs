// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;

namespace TTShang.Core.Common
{
    /// <summary>
    /// 定义了基础方法
    /// 方法包括：CU、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    public interface IServiceBaseNoKey<TEntityDto> where TEntityDto : class
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 获取全部数据
        /// </remarks>
        /// <returns></returns>
        Task<List<TEntityDto>> GetAll();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="includLocked">是否包含锁定的</param>
        /// <remarks>
        /// 获取所有可用数据，(实现<see cref="IModelDeleted"/> <see cref="IModelLocked"/>时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        Task<List<TEntityDto>> GetAllUsable(Guid? tenantId = null, bool includLocked = false);

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据分页参数，分页获取数据
        /// </remarks>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PageList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Insert(TEntityDto input);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 批量添加数据
        /// </remarks>
        /// <returns></returns>
        Task<bool> BatchInsert(IEnumerable<TEntityDto> input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Update(TEntityDto input);

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PageList<TEntityDto>> Search(PageRequest request);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// <para>判断是否存在，根据输入条件组合进行数据查询判断是否存在。</para>
        /// <para>如果是支持多租户的对象，租户仅检测自己租户下的数据，如果需要检测全局，需要重新默认方法，使用非租户数据库上下文进行查询</para> 
        /// </remarks>
        /// <param name="filterGroups"></param>
        /// <returns></returns>
        Task<bool> Exists(List<FilterGroup> filterGroups);

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        /// <returns></returns>
        Task<string> GenerateSeedData(PageRequest request);

        /// <summary>
        /// 导出
        /// </summary>
        /// <remarks>
        /// 根据搜索条件组合导出数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> Export(PageRequest request);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sendEntityEventNotity">是否发生实体变化通知（有些操作不需要影响其他关联事件）</param>
        /// <remarks>
        /// 根据模块名称删除,必须实现<see cref="IModelModule"/>
        /// </remarks>
        /// <returns></returns>
        Task<bool> DeleteByModuleName(string moduleName,bool sendEntityEventNotity=true);
    }

    /// <summary>
    /// 定义了基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto">实体对应dto</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IServiceBase<TEntityDto, TKey> : IServiceBaseNoKey<TEntityDto> where TEntityDto : class
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(TKey id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> Deletes(TKey[] ids);
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> FakeDelete(TKey id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> FakeDeletes(TKey[] ids);
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，抛出异常,code=<see cref="ExceptionCode.Data_Not_Find"/>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityDto> Get(TKey id);
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，返回null
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityDto?> Find(TKey id);
        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须实现<see cref="IModelLocked"/>才能生效）
        /// </remarks>
        /// <returns></returns>
        Task<bool> Lock(TKey id, bool islocked = true);
    }

    /// <summary>
    /// 定义了基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto">实体对应dto</typeparam>
    /// <remarks>
    /// 主键默认类型是<see cref="int"/>
    /// </remarks>
    public interface IServiceBase<TEntityDto> : IServiceBase<TEntityDto, int> where TEntityDto : class, new()
    {
    }
}