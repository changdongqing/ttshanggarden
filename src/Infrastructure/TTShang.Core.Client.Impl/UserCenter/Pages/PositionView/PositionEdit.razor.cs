// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;

namespace TTShang.Core.Client.Impl.UserCenter.Pages.PositionView
{
    public partial class PositionEdit : EditOperationDialogBase<PositionDto,int, UserCenterResource>
    {
        private IEnumerable<CodeDto>? grades;
        protected override void OnDataLoaded()
        {
            grades = DictHelper.GetCodesFromCache<PositionDto>(() => _editModel.Grade);
            base.OnDataLoaded();
        }
    }
}
