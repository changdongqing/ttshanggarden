// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;

namespace TTShang.Core.Client.Services
{
    /// <summary>
    /// 客户端服务调用基类
    /// </summary>
    /// <remarks>
    /// 客户端服务调用基类
    /// </remarks>
    public abstract class ClientServiceCaller
    {
        public readonly string controller;
        public readonly string? module;
        public readonly IApiCaller apiCaller;
        public string baseUrl = string.Empty;
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        /// <param name="module"></param>
        protected ClientServiceCaller(IApiCaller apiCaller, string controller, string? module = null)
        {
            this.module = module;
            this.apiCaller = apiCaller;
            this.controller = controller;
            this.baseUrl = $"{(string.IsNullOrEmpty(module) ? string.Empty : (module + "/"))}{(string.IsNullOrEmpty(controller) ? string.Empty : controller)}";
        }
    }

    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T">Dto</typeparam>
    /// <remarks>
    /// 实体主键类型是<see cref="int"/>
    /// <para>继承后，实现 <see cref="IServiceBase{T}"/> 基础方法 </para>
    /// </remarks>
    public abstract class ClientServiceBaseNoKey<T> : ClientServiceCaller, IServiceBaseNoKey<T> where T : class
    {
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBaseNoKey(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="module"></param>
        /// <param name="controller"></param>
        protected ClientServiceBaseNoKey(IApiCaller apiCaller, string controller, string module) : base(apiCaller, controller, module)
        {
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <remarks>
        /// 查找到所有数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<List<T>> GetAll()
        {
            return apiCaller.GetAsync<List<T>>($"{this.baseUrl}/all");
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <remarks>分页返回数据</remarks>
        /// <returns></returns>
        public virtual Task<PageList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return apiCaller.GetAsync<PageList<T>>($"{this.baseUrl}/page/{pageIndex}/{pageSize}");
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 添加一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<T> Insert(T input)
        {
            return apiCaller.PostAsync<T, T>(this.baseUrl, request: input);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 更新一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Update(T input)
        {
            return apiCaller.PutAsync<T, bool>(this.baseUrl, request: input);
        }

        /// <summary>
        /// 查询所有可以用的
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="includLocked">是否包含锁定的</param>
        /// <remarks>
        /// 查询所有可以用的记录，(实现<see cref="IModelDeleted"/> <see cref="IModelLocked"/>时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        public virtual Task<List<T>> GetAllUsable(Guid? tenantId = null, bool includLocked = false)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add($"{nameof(tenantId)}", tenantId);
            queryString.Add($"{nameof(includLocked)}", includLocked);
            return apiCaller.GetAsync<List<T>>($"{this.baseUrl}/all-usable", queryString);
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// 搜索功能数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<PageList<T>> Search(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, PageList<T>>($"{this.baseUrl}/search", request);
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// 判断是否存在，根据输入条件组合进行数据查询判断是否存在
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Task<bool> Exists(List<FilterGroup> filterGroups)
        {
            return apiCaller.PostAsync<List<FilterGroup>, bool>($"{this.baseUrl}/exists", filterGroups);
        }
        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public virtual Task<string> GenerateSeedData(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{this.baseUrl}/generate-seed-data", request);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// 导出搜索结果
        /// </remarks>
        /// <returns></returns>
        public virtual Task<string> Export(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{this.baseUrl}/export", request);
        }

        public Task<bool> BatchInsert(IEnumerable<T> input)
        {
            return apiCaller.PostAsync<IEnumerable<T>, bool>($"{this.baseUrl}/batch-insert", input);
        }

        public Task<bool> DeleteByModuleName(string moduleName, bool sendEntityEventNotity = true)
        {
            return apiCaller.DeleteAsync<bool>($"{this.baseUrl}/delete-by-modulename", new Dictionary<string, object?>() { { nameof(moduleName), moduleName }, { nameof(sendEntityEventNotity), sendEntityEventNotity } });
        }
    }
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T">Dto</typeparam>
    /// <remarks>
    /// 实体主键类型是<see cref="int"/>
    /// <para>继承后，实现 <see cref="IServiceBase{T,int}"/> 基础方法 </para>
    /// </remarks>
    public abstract class ClientServiceBase<T> : ClientServiceBase<T, int> where T : class, new()
    {
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        /// <param name="module"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller, string module) : base(apiCaller, controller, module)
        {
        }
    }
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    /// <remarks>
    /// <para>继承后，实现 <see cref="IServiceBase{T,Tkey}"/> 基础方法 </para>
    /// </remarks>
    public abstract class ClientServiceBase<T, Tkey> : ClientServiceBaseNoKey<T>, IServiceBase<T, Tkey> where T : class
    {
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }

        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        /// <param name="module"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller, string module) : base(apiCaller, controller, module)
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// 根据主键删除一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Delete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{base.baseUrl}/{id}");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Deletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{this.baseUrl}/deletes", ids);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> FakeDelete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{base.baseUrl}/fake-delete/{id}");
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> FakeDeletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{base.baseUrl}/fake-deletes", ids);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，抛出异常,code=<see cref="ExceptionCode.Data_Not_Find"/>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T> Get(Tkey id)
        {
            return apiCaller.GetAsync<T>($"{base.baseUrl}/{id}");
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，返回null
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T?> Find(Tkey id)
        {
            return apiCaller.GetAsync<T?>($"{base.baseUrl}/find/{id}");
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须实现<see cref="IModelLocked"/>才能生效）
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Lock(Tkey id, bool islocked = true)
        {
            return apiCaller.PutAsync<object, bool>($"{base.baseUrl}/{id}/lock/{islocked}");
        }

    }
}
