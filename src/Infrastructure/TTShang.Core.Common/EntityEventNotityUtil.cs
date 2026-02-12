// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using TTShang.Core.EntityFramwork.Event;
using TTShang.Core.EventBus;

namespace TTShang.Core.Common
{
    /// <summary>
    /// 实体事件通知静态类
    /// </summary>
    public static class EntityEventNotityUtil
    {
        private static IEventBus? _eventBus = null;
        private static readonly object _senderLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEventBus GetEventBus()
        {

            if (_eventBus != null)
            {
                return _eventBus;
            }
            lock (_senderLock)
            {
                if (_eventBus == null)
                {
                    _eventBus = App.GetService<IEventBus>();
                }
            }

            return _eventBus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="operateType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Task NotifyAsync<TEntityDto, TData>(EntityOperateType operateType, TData data)
        {
            EntityChangeEvent<TData> eventBase = new EntityChangeEvent<TData>(typeof(TEntityDto).FullName + operateType.ToString(), data);
            return GetEventBus().PublishAsync(eventBase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <param name="operateType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Task NotifyAsync<TEntityDto>(EntityOperateType operateType, TEntityDto data)
        {
            EntityChangeEvent<TEntityDto> eventBase = new EntityChangeEvent<TEntityDto>(typeof(TEntityDto).FullName + operateType.ToString(), data);
            return GetEventBus().PublishAsync(eventBase);
        }
        /// <summary>
        /// 通知删除
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task NotifyDeleteAsync<TEntityDto, TKey>(TKey key)
        {
            await NotifyAsync<TEntityDto, TKey>(EntityOperateType.Delete, key);
        }
        /// <summary>
        /// 通知批量删除
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static async Task NotifyDeletesAsync<TEntityDto, TKey>(IEnumerable<TKey> keys)
        {
            await NotifyAsync<TEntityDto, IEnumerable<TKey>>(EntityOperateType.Deletes, keys);
        }
        /// <summary>
        /// 通知批量删除
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static async Task NotifyDeletesAsync<TEntityDto>(IEnumerable<TEntityDto> entities)
        {
            await NotifyAsync<TEntityDto, IEnumerable<TEntityDto>>(EntityOperateType.DeletesEntity, entities);
        }
        /// <summary>
        /// 通知逻辑删除
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task NotifyFakeDeleteAsync<TEntityDto, TKey>(TKey key)
        {
            await NotifyAsync<TEntityDto, TKey>(EntityOperateType.FakeDelete, key);
        }
        /// <summary>
        /// 通知批量逻辑删除
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static async Task NotifyFakeDeletesAsync<TEntityDto, TKey>(IEnumerable<TKey> keys)
        {
            await NotifyAsync<TEntityDto, IEnumerable<TKey>>(EntityOperateType.FakeDeletes, keys);
        }
        /// <summary>
        /// 通知插入
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyInsertAsync<TEntityDto>(TEntityDto entity)
        {
            await NotifyAsync(EntityOperateType.Insert, entity);
        }
        /// <summary>
        /// 通知更新
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyUpdateAsync<TEntityDto>(TEntityDto entity)
        {
            await NotifyAsync(EntityOperateType.Update, entity);
        }
        /// <summary>
        /// 通知锁定
        /// </summary>
        /// <typeparam name="TEntityDto"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyLockAsync<TEntityDto>(TEntityDto entity)
        {
            await NotifyAsync(EntityOperateType.Lock, entity);
        }
    }
}
