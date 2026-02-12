using Microsoft.AspNetCore.Components;

namespace Gardener.Iot.Client.Pages.DeviceGroupView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DeviceGroup : TreeTableBase<DeviceGroupDto, int, DeviceGroupEdit, IotLocalResource>
    {
        [Inject]
        private IDeviceGroupService deviceGroupService { get; set; } = null!;
        protected override ICollection<DeviceGroupDto>? GetChildren(DeviceGroupDto dto)
        {
            return dto.Children;
        }

        protected override int? GetParentKey(DeviceGroupDto dto)
        {
            return dto.ParentId;
        }

        protected override Task<List<DeviceGroupDto>> GetTree()
        {
            return deviceGroupService.GetTree(true);
        }

        protected override void SetChildren(DeviceGroupDto dto, ICollection<DeviceGroupDto>? children)
        {
            dto.Children = children;
        }

        protected override ICollection<DeviceGroupDto>? SortChildren(ICollection<DeviceGroupDto>? children)
        {
            return children?.OrderBy(x=>x.Order).ToList();
        }
    }
}
