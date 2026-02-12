// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos;

namespace TTShang.Core.Util.Extensions
{
    /// <summary>
    /// linq扩展
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// 使用异步遍历处理数据
        /// </summary>
        /// <typeparam name="T">需要遍历的基类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="func">Lambda表达式</param>
        /// <returns> </returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
           return list.ForEachIsAsync(func);
        }
        /// <summary>
        /// 使用异步遍历处理数据
        /// </summary>
        /// <typeparam name="T">需要遍历的基类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="func">Lambda表达式</param>
        /// <remarks>
        /// 避免与Ant冲突
        /// </remarks>
        /// <returns> </returns>
        public static Task ForEachIsAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            List<Task> tasks = new List<Task>();
            foreach (T value in list)
            {
                tasks.Add(func(value));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// In: PageIndex + PageSize
        /// Out: PageList
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PageList<TEntity> ToPageList<TEntity>(this IEnumerable<TEntity> entities, PageRequest request)
            where TEntity : class, new()
        {
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;

            var totalCount = entities.Count();
            var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PageList<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }
    }
}
