// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Entities;
using Gardener.Core.Api.Impl.Audit.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gardener.Core.Api.Impl.EntityFramwork.Internal
{
    /// <summary>
    /// ef审计数据处理器
    /// </summary>
    internal class EFAuditEntityDataHander
    {

        private IEnumerable<AuditEntity>? _auditEntitys;

        private readonly IAuditService? auditService;

        public EFAuditEntityDataHander(IAuditService? auditService=null)
        {
            this.auditService = auditService;
        }

        /// <summary>
        /// 保存实体审计数据过程
        /// </summary>
        /// <param name="entitys"></param>
        internal void SavingChangesEvent(IEnumerable<EntityEntry> entitys)
        {
            if (auditService==null || entitys == null || !entitys.Any())
            {
                return;
            }
            // 获取当前事件对应上下文
            // 获取所有实体  
            entitys = entitys.Where(w =>
                w.State == EntityState.Added || w.State == EntityState.Modified || w.State == EntityState.Deleted
            );
            if (!entitys.Any()) return;
            List<AuditEntity> auditEntities = new List<AuditEntity>();
            foreach (var entity in entitys)
            {
                // 获取实体的类型
                var entityType = entity.Entity.GetType();
                if (entityType == null) { continue; }
                //跳过
                if (entityType.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) { continue; }
                // 获取实体当前的值
                PropertyValues currentValues = entity.CurrentValues;
                AuditEntity auditEntity = new AuditEntity()
                {
                    TypeName = entityType.FullName ?? string.Empty,
                    Name = entityType.GetDescription() ?? string.Empty,
                    AuditFunctionId = Guid.NewGuid(),
                    CurrentValues = currentValues,
                    OldValues = entity.GetDatabaseValues(),
                    CreatedTime = DateTimeOffset.Now,
                };
                switch (entity.State)
                {
                    case EntityState.Modified: auditEntity.OperationType = EntityOperateType.Update; break;
                    case EntityState.Added: auditEntity.OperationType = EntityOperateType.Insert; break;
                    case EntityState.Deleted: auditEntity.OperationType = EntityOperateType.Delete; break;
                }
                //记录下变化的实体
                auditEntities.Add(auditEntity);
            }
            _auditEntitys = auditEntities;
        }
        /// <summary>
        /// 保存实体审计数据
        /// </summary>
        /// <returns></returns>
        internal Task SavedChangesEvent()
        {
            if (auditService == null || _auditEntitys == null)
            {
                return Task.CompletedTask;
            }
            foreach (AuditEntity entity in _auditEntitys)
            {
                var (pkValues, auditProperties) = GetAuditProperties(entity.OperationType, entity.CurrentValues, entity.OldValues);
                entity.DataId = string.Join(',', pkValues);
                entity.AuditProperties = auditProperties;
            }
           return auditService.DataSaveEnd(_auditEntitys);
        }
        /// <summary>
        /// 获取属性审计信息
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="currentValues"></param>
        /// <param name="originalValues"></param>
        /// <returns></returns>
        private (List<object>, ICollection<AuditProperty>) GetAuditProperties(EntityOperateType operationType, PropertyValues currentValues, PropertyValues? originalValues)
        {
            ICollection<AuditProperty> auditProperties = new List<AuditProperty>();
            List<object> pkValues = new List<object>();

            // 获取实体的所有属性，排除【NotMapper】属性
            var props = currentValues.Properties;
            // 遍历所有的属性
            foreach (var prop in props)
            {
                //不需要审计
                if (prop.PropertyInfo == null || prop.PropertyInfo.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) continue;
                // 获取属性值
                string propName = prop.Name;
                // 获取属性当前的值
                var newValue = currentValues[propName];

                //添加的时候，空值字段就不记录了
                if (EntityOperateType.Insert.Equals(operationType) && string.IsNullOrEmpty(ValueToString(newValue))) continue;
                object? oldValue = null;
                if (originalValues != null)
                {
                    oldValue = originalValues[propName];
                }
                //是主键
                IKey? pk = prop.FindContainingPrimaryKey();
                if (pk != null && (newValue != null || oldValue != null))
                {
                    if (newValue != null)
                    {
                        pkValues.Add(newValue);
                    }
                    else if (oldValue != null)
                    {
                        pkValues.Add(oldValue);
                    }

                }
                //更新的话需对比到底有没有变化
                if (operationType.Equals(EntityOperateType.Update) &&
                        (
                            newValue == null && oldValue == null
                            ||
                            newValue != null && newValue.Equals(oldValue)
                        )
                    ) continue;
                var property = new AuditProperty()
                {
                    DisplayName = prop.PropertyInfo.GetDescription() ?? string.Empty,
                    FieldName = propName,
                    OriginalValue = ValueToString(oldValue),
                    CreatedTime = DateTimeOffset.Now
                };
                if (!operationType.Equals(EntityOperateType.Delete))
                {
                    property.NewValue = ValueToString(newValue);
                }
                Type fieldType = prop.PropertyInfo.PropertyType;
                if (fieldType.IsNullableType())
                {
                    property.DataType = fieldType.GetGenericArguments()[0].Name;
                }
                else
                {
                    property.DataType = prop.PropertyInfo.PropertyType.Name;
                }
                auditProperties.Add(property);
            }

            return (pkValues, auditProperties);
        }
        /// <summary>
        /// 格式化各种类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string? ValueToString(object? value)
        {
            if (value == null) return null;
            if (value is DateTime)
            {
                if (value.Equals(DateTime.MinValue)) return null;
            }
            else if (value is DateTimeOffset)
            {
                if (value.Equals(DateTimeOffset.MinValue)) return null;
            }
            else if (value is Guid)
            {
                if (value.Equals(Guid.Empty)) return null;
            }
            else if (value.GetType().IsSubclassOf(typeof(Enum)))
            {
                //枚举展示的是Description或名字
                return EnumHelper.GetEnumDescriptionOrName((Enum)value);
            }
            return value.ToString();

        }
    }
}
