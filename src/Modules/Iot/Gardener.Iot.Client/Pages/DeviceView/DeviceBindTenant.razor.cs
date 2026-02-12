// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attributes;
using Gardener.Core.Client.Authorization;
using Gardener.Core.UserCenter.Dtos;
using Gardener.Core.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Iot.Client.Pages.DeviceView
{

    internal class DeviceBindTenantInputDto: DeviceBindTenantInput
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public new Guid? DeviceId { get; set; }
    }
    public partial class DeviceBindTenant : OperationDialogBase<Guid?, bool, IotLocalResource>
    {
        /// <summary>
        /// 租户列表
        /// </summary>
        private IEnumerable<SystemTenantDto>? _tenants { get; set; }
        /// <summary>
        /// 租户服务    
        /// </summary>
        [Inject]
        private ITenantService tenantService { get; set; } = null!;
        /// <summary>
        /// 身份状态管理
        /// </summary>
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IDeviceService deviceService { get; set; } = null!;

        [Inject]
        private IClientMessageService messageService { get; set; } = null!;

        private DeviceBindTenantInputDto _editModel = new DeviceBindTenantInputDto();

        protected override void OnInitialized()
        {
            if(this.Options.HasValue)
            {
                _editModel.DeviceId = this.Options.Value;
            }
            base.OnInitialized();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await StartLoading();

            if (!IsTenant())
            {
                await LoadTenants();
            }
            else
            {
                var user = authenticationStateManager.GetCurrentUser();
                if (user != null && user.TenantId.HasValue)
                {
                    _editModel.TenantId = user.TenantId.Value;
                }
            }
            await StopLoading();
        }

        /// <summary>
        /// 加载租户数据
        /// </summary>
        /// <returns></returns>
        protected async Task LoadTenants()
        {
            _tenants = await tenantService.GetAll();

        }
        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsTenant()
        {
            bool isTenant = authenticationStateManager.CurrentUserIsTenant();
            return isTenant;
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected virtual async Task OnFormFinish(EditContext editContext)
        {
            await StartLoading();
            if (Guid.Empty.Equals(_editModel.DeviceId))
            {
                return;
            }
            bool result = await deviceService.BindTenant(_editModel.Adapt<DeviceBindTenantInput>());
            if (result)
            {
                messageService.Success(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Success)));
                await base.CloseAsync(true);
            }
            else
            {
                messageService.Error(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Fail)));
            }
            await StopLoading();
           
        }

    }
}
