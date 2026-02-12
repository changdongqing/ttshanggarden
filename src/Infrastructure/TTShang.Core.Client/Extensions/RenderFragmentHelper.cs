// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Rendering;

namespace TTShang.Core.Client.Extensions
{
    public static class RenderFragmentHelper
    {
        public static RenderFragment ToMarkupRenderFragment(this string value)
        {
            return delegate (RenderTreeBuilder builder)
            {
                builder.AddMarkupContent(1, value);
            };
        }
    }
}
