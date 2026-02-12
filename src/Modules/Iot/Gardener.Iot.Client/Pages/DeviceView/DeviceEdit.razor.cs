// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace Gardener.Iot.Client.Pages.DeviceView
{
    public partial class DeviceEdit : EditOperationDialogBase<DeviceDto, Guid, IotLocalResource>
    {
        [Inject]
        IProductService productService { get; set; } = null!;


        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string _deviceGroupId
        {
            get { return _editModel.DeviceGroupId?.ToString() ?? string.Empty; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.DeviceGroupId = int.Parse(value);
                }
                else
                {
                    _editModel.DeviceGroupId = null;
                }

            }
        }

        //树
        List<DeviceGroupDto> allDeviceGroupDatas = new List<DeviceGroupDto>();
        List<ProductDto> productDatas = new List<ProductDto>();


        [Inject]
        private IDeviceGroupService deviceGroupService { get; set; } = null!;
        protected override void OnInitialized()
        {
            //注册需要验证唯一性的字段
            _uniqueVerificationTool
                .AddField(x => x.ClientId);
            base.OnInitialized();
        }
        /// <summary>
        /// 加载数据中，加载设备组
        /// </summary>
        /// <returns></returns>
        protected override async Task OnDataLoadingAsync()
        {
            if (AuthenticationStateManager.CheckCurrentUserHaveResource("iot_device_group"))
            {
                allDeviceGroupDatas = await deviceGroupService.GetTree(true);
            }
            if (AuthenticationStateManager.CheckCurrentUserHaveResource("iot_product"))
            {
                productDatas = await productService.GetAllUsable(includLocked: true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDataLoaded()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.SecretKey = RandomCodeCreator.CreatRandomNumAndChar(14);
            }
            base.OnDataLoaded();
        }
        private HashidsHelper hashidsHelper = HashidsHelper.CreateHelper(alphabet: "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");
        private void OnSelectedItemChanged(ProductDto productDto)
        {
            string timespan = hashidsHelper.EncodeLong(IdHelper.GetNextId());
            if (string.IsNullOrEmpty(_editModel.ClientId))
            {
                _editModel.ClientId = $"{productDto.ProductType.ToUpper()}-{timespan}-{RandomCodeCreator.CreatRandomNumAndChar(4).ToUpper()}";
            }
            if (string.IsNullOrEmpty(_editModel.Name))
            {
                _editModel.Name = productDto.ProductName + timespan;
            }
            if (string.IsNullOrEmpty(_editModel.Account))
            {
                _editModel.Account = RandomCodeCreator.CreatRandomChar(5).ToLower();
            }
        }
    }
}
