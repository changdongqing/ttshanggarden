// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemAsset.Extensions;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.UserCenter.Pages.RoleView
{
    public partial class RoleResourceEdit : OperationDialogBase<RoleDto, bool>
    {
        private Tree<ResourceDto>? _tree;
        private bool _isExpanded;
        private int _roleId = 0;
        private List<ResourceDto> _resources = new List<ResourceDto>();
        [Inject]
        private IResourceService ResourceService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private IRoleService RoleService { get; set; } = null!;
        /// <summary>
        /// 默认选择
        /// </summary>
        private string[] _defaultCheckedKeys { get; set; } = null!;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.StartLoading();
            _roleId = this.Options.Id;
            if (_roleId > 0)
            {
                var t1 = RoleService.GetResource(_roleId);
                var t2 = ResourceService.GetTree(tenantId: this.Options.TenantId);
                //已有资源
                var roleResourceResult = await t1;
                if (roleResourceResult != null && roleResourceResult.Any())
                {
                    _defaultCheckedKeys = roleResourceResult.Where(dto => dto.Children == null || !dto.Children.Any()).Select(dto => dto.Id.ToString()).ToArray();
                }
                //资源树
                var resourceResult = await t2;
                if (resourceResult == null)
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Resource), nameof(SharedLocalResource.Load), nameof(SharedLocalResource.Fail)));
                    await base.StopLoading();
                    return;
                }
                _resources.AddRange(resourceResult);
            }
            await base.StopLoading();
        }


        /// <summary>
        /// 点击取消
        /// </summary>
        private async Task OnCancelClick()
        {
            await base.FeedbackRef.CloseAsync(false);
        }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task OnSaveClick()
        {
            _dialogLoading.Start();

            List<Guid> resourceIds = new List<Guid>();

            if (_tree != null && _tree.CheckedKeys.Length > 0)
            {
                _tree.CheckedKeys.ForEach(x =>
                {
                    TreeNode<ResourceDto> node = _tree.FindFirstOrDefaultNode(node => { return node.Key.Equals(x); }, true);
                    if (node != null)
                    {
                        resourceIds.Add(node.DataItem.Id);
                        List<ResourceDto>? predecessors = node.DataItem.GetPredecessors(_resources);
                        if (predecessors != null)
                        {
                            resourceIds.AddRange(predecessors.Select(x=>x.Id));
                        }
                    }
                });
            }
            //删除所有资源
            var result = await RoleService.Resource(_roleId, resourceIds.Distinct().ToArray());
            if (result)
            {
                MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Success)));
                await base.FeedbackRef.CloseAsync(true);
            }
            else
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Fail)));
            }
            _dialogLoading.Stop();
        }

        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private void OnExpandClick()
        {
            if (_tree == null) return;
            _dialogLoading.Start();
            _isExpanded = !_isExpanded;
            //操作所有的节点
            if (_isExpanded)
            {
                _tree.ExpandAll();
            }
            else
            {
                _tree.CollapseAll();
            }
            _dialogLoading.Stop();
        }
    }
}
