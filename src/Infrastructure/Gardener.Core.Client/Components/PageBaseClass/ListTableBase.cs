// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using Gardener.Core.Dtos.Constraints;
using System.Reflection;

namespace Gardener.Core.Client.Components.PageBaseClass
{
    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : MultiTenantTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : class, new()
    {
        /// <summary>
        /// 显示总数
        /// </summary>
        protected int _total = 0;

        /// <summary>
        /// 控制分页每页数量
        /// </summary>
        protected int _pageSize = ClientConstant.PageSize;

        /// <summary>
        /// 控制页码
        /// </summary>
        protected int _pageIndex = 1;

        #region override mothed
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        /// <summary>
        /// OnParametersSetAsync
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override Task OnParametersSetAsync()
        {
            //this.firstRenderAfter = true;
            //await ReLoadTable();
            return base.OnParametersSetAsync();
        }
        #endregion

        /// <summary>
        /// 在构建完成前配置搜索请求-基类可以重写
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected virtual void ConfigurationPageRequest(PageRequest pageRequest)
        {
            //set pageRequest
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="filterGroups">附加的参数</param>
        /// <returns></returns>
        protected override PageRequest GetPageRequest()
        {
            PageRequest pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            //如果有搜索条件提供者 就拼接上
            if (_tableSearchFilterGroupProviders.Any())
            {
                _tableSearchFilterGroupProviders.ForEach(p =>
                {
                    var items = p.Invoke();
                    if (items != null)
                    {
                        pageRequest.FilterGroups.AddRange(items);
                    }
                });
            }
            ConfigurationPageRequest(pageRequest);
            return pageRequest;
        }


        /// <summary>
        /// 点击TableSearch搜索
        /// </summary>
        /// <param name="filterGroups"></param>
        protected virtual Task OnTableSearch(List<FilterGroup> filterGroups)
        {
            //TableSearch的搜索条件已注册到=>_tableSearchFilterGroupProviders,只需要触发加载即可。
            return ReLoadTable(true);
        }

        /// <summary>
        /// 获取当前Table页码
        /// </summary>
        /// <returns></returns>
        protected int GetCurrentTablePageIndex()
        {
            return _table?.GetQueryModel().PageIndex ?? 1;
        }

        #region 重新加载table
        /// <summary>
        /// 重新加载table-刷新当前页
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual Task ReLoadTable()
        {
            return ReLoadTable(false);
        }
        /// <summary>
        /// 重新加载table-刷新当前页
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual Task ReLoadTable(bool firstPage)
        {
            return ReLoadTable(firstPage ? 1 : null);
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="goToPageIndex">可以指定页码,如果为null刷新当前页</param>
        /// <returns></returns>
        protected virtual Task ReLoadTable(int? goToPageIndex)
        {
            if (_table == null)
            {
                return Task.CompletedTask;
            }
            QueryModel queryModel = _table.GetQueryModel();

            if (goToPageIndex == null)
            {
                //未指定，刷新当前页
                goToPageIndex = queryModel.PageIndex;
            }
            //刷新

            if (_pageIndex != goToPageIndex.Value)
            {
                _pageIndex = goToPageIndex.Value;
                //触发OnChange
                return base.RefreshPageDom();
            }
            PageRequest pageRequest = GetPageRequest();
            return ReLoadTable(pageRequest, true);
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(PageRequest pageRequest, bool forceRender = false)
        {
            StartTableLoading(forceRender);
            var pagedListResult = await BaseService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                IEnumerable<TDto> _dataTemps = pagedList.Items ?? new List<TDto>(0);
                if (_dataTemps.Any())
                {
                    //如果有租户数据，装配一下
                    if (typeof(TDto).IsAssignableTo(typeof(IModelTenant)))
                    {
                        foreach (TDto item in _dataTemps)
                        {
                            if (item is IModelTenant modelTenant)
                            {
                                modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                            }
                        }
                    }
                }

                await PageListDataHadnle(_dataTemps);
                _total = pagedList.TotalCount;
                _datas = _dataTemps;
                _table?.SetSelection(null);
            }
            else
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Load), nameof(SharedLocalResource.Fail)));
            }
            StopTableLoading(forceRender);
        }

        #endregion

        /// <summary>
        /// 列表接口返回后，对页面列表数据进行处理
        /// </summary>
        /// <param name="datas"></param>
        protected virtual Task PageListDataHadnle(IEnumerable<TDto> datas)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// table查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected virtual Task OnChange(QueryModel<TDto> queryModel)
        {
            PageRequest pageRequest = GetPageRequest();
            return ReLoadTable(pageRequest, false);
        }

        #region Delete

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trueDelete">物理删除</param>
        /// <returns></returns>
        protected virtual async Task OnClickDelete(TKey id, bool trueDelete)
        {
            var f = await ConfirmService.YesNoDelete();
            if (f == ConfirmResult.Yes)
            {
                var result = trueDelete ? await BaseService.Delete(id) : await BaseService.FakeDelete(id);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                    PageRequest pageRequest = GetPageRequest();
                    //当前页被删完了
                    int pageIndex = GetCurrentTablePageIndex();
                    if (pageIndex > 1 && _datas?.Count() == 1)
                    {
                        pageIndex = pageIndex - 1;
                    }
                    await ReLoadTable(pageIndex);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                }
            }

        }

        /// <summary>
        /// 点击删除选中按钮(物理删除)
        /// </summary>
        /// <param name="trueDelete">物理删除</param>
        /// <returns></returns>
        protected virtual async Task OnClickDeletes(bool trueDelete)
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {

                    var result = trueDelete ? await BaseService.Deletes(_selectedRows.Select(x => GetKey(x)).ToArray()) : await BaseService.FakeDeletes(_selectedRows.Select(x => GetKey(x)).ToArray());
                    if (result)
                    {
                        MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                        //当前页被删完了
                        int pageIndex = GetCurrentTablePageIndex();
                        if (pageIndex > 1 && _datas?.Count() == 1)
                        {
                            pageIndex = pageIndex - 1;
                        }
                        await ReLoadTable(pageIndex);
                    }
                    else
                    {
                        MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                    }

                }
                _deletesBtnLoading = false;
            }
        }

        #region Fake delete

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        protected virtual Task OnClickFakeDelete(TKey id)
        {
            return OnClickDelete(id, false);
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected virtual Task OnClickFakeDeletes()
        {
            return OnClickDeletes(false);
        }

        #endregion

        #region True Delete
        /// <summary>
        /// 点击删除按钮(物理删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual Task OnClickTrueDelete(TKey id)
        {
            return OnClickDelete(id, true);
        }

        /// <summary>
        /// 点击删除选中按钮(物理删除)
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnClickTrueDeletes()
        {
            return OnClickDeletes(true);
        }
        #endregion

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        protected virtual Task OnClickDelete(TKey id)
        {
            return OnClickDelete(id, ClientConstant.TableListDeleteUseTrueDelete);
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected virtual Task OnClickDeletes()
        {
            return OnClickDeletes(ClientConstant.TableListDeleteUseTrueDelete);
        }
        #endregion

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual async Task OnClickShowSeedData<TShowSeedDataDrawer>() where TShowSeedDataDrawer : FeedbackComponent<ShowCodeOptions, bool>
        {
            PageRequest pageRequest = GetPageRequest();
            pageRequest.PageSize = int.MaxValue;
            pageRequest.PageIndex = 1;
            Task<string> seedData = BaseService.GenerateSeedData(pageRequest);
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            drawerSettings.Width = "1300";
            drawerSettings.ModalMaximizable = true;
            await OpenOperationDialogAsync<TShowSeedDataDrawer, ShowCodeOptions, bool>(Localizer[nameof(SharedLocalResource.SeedData)], new ShowCodeOptions() { Code = seedData }, operationDialogSettings: drawerSettings);
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual Task OnClickShowSeedData()
        {
            return OnClickShowSeedData<ShowCode>();
        }

        /// <summary>
        /// 根据搜索实体获取搜索条件
        /// </summary>
        /// <typeparam name="TSearchDto"></typeparam>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        protected List<FilterGroup> GetCustomSearchFilterGroups<TSearchDto>(TSearchDto searchDto)
        {
            List<FilterGroup> filterGroups = new List<FilterGroup>();

            Type type = typeof(TSearchDto);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                Type fieldType = property.PropertyType.GetNonNullableType();
                if (property.GetCustomAttribute<CustomSearchFieldAttribute>() != null
                    && (fieldType.IsPrimitive
                        || fieldType.IsEnum
                        || fieldType.Equals(typeof(string)) // input, select
                        || fieldType.IsEnumerable() // select multiple
                        || fieldType.Equals(typeof(Guid))
                        || fieldType.Equals(typeof(DateTime))
                        || fieldType.Equals(typeof(DateTimeOffset))))
                {
                    string fieldName = property.Name;

                    // string
                    if (fieldType.Equals(typeof(string)))
                    {
                        // Get value
                        var value = searchDto.GetPropertyValue<TSearchDto, string>(property.Name);
                        // Set filter
                        if (!string.IsNullOrEmpty(value))
                        {
                            // Filter group by field
                            // 每一个字段一个 Filter Group
                            FilterGroup filterGroup = new FilterGroup();

                            FilterRule rule = new FilterRule()
                            {
                                Field = fieldName,
                                Value = value.CastTo(fieldType),
                                Operate = FilterOperate.Contains
                            };

                            filterGroup.AddRule(rule);

                            filterGroups.Add(filterGroup);
                        }
                    }
                    else if (fieldType.IsEnumerable())
                    {
                        // Get values
                        var values = searchDto.GetPropertyValue<TSearchDto, IEnumerable<string>>(property.Name);
                        // Set filters
                        if (values != null && values.Any())
                        {
                            // Filter group by field
                            // 每一个字段一个 Filter Group
                            FilterGroup filterGroup = new FilterGroup();

                            foreach (string valueTemp in values)
                            {
                                FilterRule rule = new FilterRule()
                                {
                                    Field = fieldName,
                                    Value = valueTemp.CastTo(typeof(string)), // IEnumerable<string>
                                    Condition = FilterCondition.Or,
                                    Operate = FilterOperate.Contains
                                };
                                filterGroup.AddRule(rule);
                            }

                            filterGroups.Add(filterGroup);
                        }
                    }
                }
            }

            return filterGroups;
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey, TLocalResource> : ListTableBase<TDto, TKey, TLocalResource, TKey, bool>
        where TDto : class, new()
    {
    }

    /// <summary>
    /// 列表table基类-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// 本地化资源 默认使用<see cref="SharedLocalResource"/>
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey> : ListTableBase<TDto, TKey, SharedLocalResource>
        where TDto : class, new()
    {
    }
}
