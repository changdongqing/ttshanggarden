// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Email.Dtos;
using TTShang.Core.Email.Resources;

namespace TTShang.Core.Client.Impl.Email.Pages
{
    public partial class EmailServerConfigEdit : EditOperationDialogBase<EmailServerConfigDto, Guid, EmailLocalResource>
    {
        private IEnumerable<string> _tags
        {
            get
            {
                return _editModel.Tags?.Split(",") ?? new string[0];
            }
            set
            {

                value= value.Where(t => !string.IsNullOrEmpty(t)).ToList();
                _editModel.Tags = string.Join(",", value);

            }
        }

    }
}
