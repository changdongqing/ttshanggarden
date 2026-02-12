// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Components.Validation;
using Gardener.Core.Dtos.Constraints;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Client.OperationDialog
{
    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源类</typeparam>
    /// <typeparam name="TOperationDialogInput">弹框输入参数，需要继承 OperationDialogInput<TKey></typeparam>
    /// <typeparam name="TOperationDialogOutput"></typeparam>
    public class EditOperationDialogBase<TDto, TKey, TLocalResource, TOperationDialogInput, TOperationDialogOutput> : OperationDialogBase<TOperationDialogInput, TOperationDialogOutput, TLocalResource>
        where TDto : class, new()
        where TOperationDialogInput : OperationDialogInput<TKey>, new()
        where TOperationDialogOutput : OperationDialogOutput, new()
    {
        [Inject]
        protected IServiceBase<TDto, TKey> BaseService { get; set; } = null!;
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        protected ConfirmService ConfirmService { get; set; } = null!;
        [Inject]
        protected DrawerService DrawerService { get; set; } = null!;
        /// <summary>
        /// 租户服务    
        /// </summary>
        [Inject]
        protected ITenantService tenantService { get; set; } = null!;
        /// <summary>
        /// 身份状态管理
        /// </summary>
        [Inject]
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 当前正在编辑的对象
        /// </summary>
        protected TDto _editModel = new();

        /// <summary>
        /// form对象
        /// </summary>
        protected Form<TDto>? _editForm;

        /// <summary>
        /// 租户列表
        /// </summary>
        protected IEnumerable<SystemTenantDto>? _tenants { get; set; }

        /// <summary>
        /// 唯一值验证工具
        /// </summary>
        protected UniqueValueValidationDescription<TDto> _uniqueVerificationTool = new UniqueValueValidationDescription<TDto>();
        /// <summary>
        /// 唯一值验证规则
        /// </summary>
        protected FormValidationRule? _uniqueValueValidatorRule;
        protected FormValidationRule? _uniqueValueValidatorRuleNumber;
        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            _uniqueVerificationTool
                .SetModelProviders(() => _editModel)
                .SetExistsFun(x => BaseService.Exists(x));

            Func<FormValidationContext, ValidationResult?> validator = (validationContext) =>
            {
                var (unique, combs) = _uniqueVerificationTool.IsUnique(validationContext.FieldName);
                if (unique)
                {
                    return ValidationResult.Success;
                }
                string errorMessage = string.Format(ValidateErrorMessagesResource.UniqueValueValidationError, Localizer[validationContext.FieldName]);
                if (combs != null && combs.Any())
                {
                    errorMessage += "，" + string.Format(Localizer[nameof(SharedLocalResource.UniqueGroupIncludes)], string.Join(",", combs.Select(x => Localizer[x])));
                }
                var result = new ValidationResult(errorMessage, new string[] { validationContext.FieldName });
                return result;
            };

            _uniqueValueValidatorRule = new FormValidationRule
            {
                Validator = validator,
                //Type = FormFieldType.String
            };
            _uniqueValueValidatorRuleNumber = new FormValidationRule
            {
                Validator = validator,
                Type = FormFieldType.Number
            };
            //编辑时，排除自己的主键
            if (OperationDialogInputType.Edit.Equals(this.Options.Type) && _editModel is IModelId<TKey> temp)
            {
                _uniqueVerificationTool.ExcludeField(x => ((IModelId<TKey>)x).Id);
            }
            base.OnInitialized();
        }

        /// <summary>
        /// 加载当前数据<see cref="EditOperationDialogBase{TDto, TKey, TLocalResource, TOperationDialogInput, TOperationDialogOutput}.LoadEditModelData"/>
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await StartLoading();
            OnDataLoadBefor();
            await OnDataLoadBeforAsync();
            var tasks = new List<Task>
            {
                LoadEditModelData(),
                OnDataLoadingAsync()
            };

            if (!IsTenant())
            {
                tasks.Add(LoadTenants());
            }
            tasks.Add(base.OnInitializedAsync());
            await Task.WhenAll(tasks);
            OnDataLoaded();
            await OnDataLoadedAsync();
            await StopLoading();
        }
        /// <summary>
        /// 加载数据中
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnDataLoadingAsync()
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 加载数据后
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnDataLoadedAsync()
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 加载数据前
        /// </summary>
        /// <returns></returns>
        protected virtual void OnDataLoadBefor()
        {
        }
        /// <summary>
        /// 加载数据前
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnDataLoadBeforAsync()
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 加载数据后
        /// </summary>
        /// <returns></returns>
        protected virtual void OnDataLoaded()
        {
        }
        /// <summary>
        /// 判断输入类型是哪些
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        protected bool OperationIs(OperationDialogInputType types)
        {
            return (Options.Type & types) != 0;
        }
        /// <summary>
        /// 操作是否仅读数据
        /// </summary>
        protected bool operationIsOnlyReadData
        {
            get
            {

                return OperationIs(OperationDialogInputType.Select);
            }
        }
        /// <summary>
        /// 操作可以变更数据
        /// </summary>
        protected bool operationCanChangeData
        {
            get
            {

                return OperationIs(OperationDialogInputType.Add | OperationDialogInputType.Edit);
            }
        }
        /// <summary>
        /// 加载编辑对象数据
        /// </summary>
        /// <returns></returns>
        protected async Task LoadEditModelData()
        {
            if (Options.Type.Equals(OperationDialogInputType.Edit) || Options.Type.Equals(OperationDialogInputType.Select))
            {
                TKey? id = Options.Data;
                if (id != null)
                {
                    //更新 回填数据
                    var model = await BaseService.Get(id);
                    if (model != null)
                    {
                        //赋值给编辑对象
                        model.Adapt(_editModel);
                    }
                    else
                    {
                        MessageService.Error(Localizer[nameof(SharedLocalResource.Data_Not_Find)]);
                    }
                }

            }
        }

        /// <summary>
        /// 加载租户数据
        /// </summary>
        /// <returns></returns>
        protected async Task LoadTenants()
        {
           var tenants = await tenantService.GetAll();
            _tenants = tenants.OrderByDescending(x => x.CreatedTime);

        }
        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsTenant()
        {
            bool isTenant = AuthenticationStateManager.CurrentUserIsTenant();
            return isTenant;
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected virtual Task OnFormCancel()
        {
            TOperationDialogOutput operationDialogOutput = new TOperationDialogOutput();
            operationDialogOutput.IsCancel();
            return CloseAsync(operationDialogOutput);
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected virtual async Task OnFormFinish(EditContext editContext)
        {
            await StartLoading();
            var operationDialogOutput = new OperationDialogOutput<TKey>();
            //开始请求
            if (Options.Type.Equals(OperationDialogInputType.Add))
            {
                //添加
                var result = await BaseService.Insert(_editModel);

                if (result != null)
                {
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Add), nameof(SharedLocalResource.Success)));
                    operationDialogOutput.IsSucceed(GetKey(result));
                    await CloseAsync(operationDialogOutput as TOperationDialogOutput);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Add), nameof(SharedLocalResource.Fail)));
                }
            }
            else
            {
                //修改
                var result = await BaseService.Update(_editModel);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Success)));
                    operationDialogOutput.IsSucceed();
                    await CloseAsync(operationDialogOutput as TOperationDialogOutput);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Fail)));
                }
            }
            await StopLoading();
        }
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual TKey GetKey(TDto dto)
        {
            if (dto is IModelId<TKey> temp)
            {
                return temp.Id;
            }
            else
            {
                throw new ArgumentException($"{Localizer[nameof(SharedLocalResource.Error)]}:{typeof(TDto).Name} no implement {nameof(IModelId<TKey>)}");
            }
        }

        /// <summary>
        /// 当验证之前
        /// </summary>
        /// <remarks>
        /// 当使用<see cref="OnGoToSubmit"/>提交表单时，验证之前会执行该方法， 如果返回false,则不提交表单
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnVerificationBefor() { return Task.FromResult(true); }

        /// <summary>
        /// 执行提交表单操作
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnGoToSubmit()
        {
            await _uniqueVerificationTool.CheckAllFields();

            if (await OnVerificationBefor())
            {
                _editForm?.Submit();
            }
        }
    }

    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源类</typeparam>
    /// <remarks>
    /// <para>
    /// 弹框输入参数，默认是 <![CDATA[OperationDialogInput<Tkey>]]>
    /// </para>
    /// <para>
    /// 弹框输出参数，默认是 <see cref="OperationDialogOutput"/>
    /// </para>
    /// </remarks>
    public class EditOperationDialogBase<TDto, TKey, TLocalResource> : EditOperationDialogBase<TDto, TKey, TLocalResource, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>
        where TDto : class, new()
    {

    }

    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// <para>本地化资源类,默认是 <see cref="SharedLocalResource"/></para>
    /// <para>弹框输入参数，默认是 <![CDATA[OperationDialogInput<Tkey>]]></para>
    /// <para>弹框输出参数，默认是 <see cref="OperationDialogOutput"/></para>
    /// </remarks>
    public class EditOperationDialogBase<TDto, TKey> : EditOperationDialogBase<TDto, TKey, SharedLocalResource>
        where TDto : class, new()
    {

    }
}
