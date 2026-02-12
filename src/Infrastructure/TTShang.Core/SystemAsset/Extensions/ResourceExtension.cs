// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.SystemAsset.Extensions
{
    /// <summary>
    /// 资源扩展方法
    /// </summary>
    public static class ResourceExtension
    {
        /// <summary>
        ///  获取所有前辈
        /// </summary>
        /// <param name="find"></param>
        /// <param name="resources"></param>
        /// <param name="predecessors"></param>
        /// <returns></returns>
        public static List<ResourceDto>? GetPredecessors(this ResourceDto find, List<ResourceDto> resources, List<ResourceDto>? predecessors = null)
        {
            predecessors ??= new List<ResourceDto>();

            foreach (var item in resources)
            {
                if (item.Id.Equals(find.Id))
                {
                    return predecessors;
                }
                var pre = new List<ResourceDto>(predecessors);
                pre.Add(item);
                if (item.Children != null && item.Children.Any())
                {
                    var result = find.GetPredecessors(item.Children.ToList(), pre);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
