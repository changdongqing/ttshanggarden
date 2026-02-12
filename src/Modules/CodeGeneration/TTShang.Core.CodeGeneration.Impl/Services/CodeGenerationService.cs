// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using TTShang.Core.CodeGeneration.Dtos;
using TTShang.Core.CodeGeneration.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using TTShang.Core.Util.Extensions;
using TTShang.Core.Attributes;
using Furion.FriendlyException;
using TTShang.Core.CodeGeneration.Resources;
using Furion.ViewEngine;
using System.Reflection;
using TTShang.Core.Dtos.Constraints;

namespace TTShang.Core.CodeGeneration.Impl.Services
{
    /// <summary>
    /// 代码生成服务
    /// </summary>
    [ApiDescriptionSettings(groups: "CodeGeneration", Module = "code-gen")]
    public class CodeGenerationService : ICodeGenerationService
    {
        private readonly IEntityConfigService _entityConfigService;
        private readonly IFieldConfigService _fieldConfigService;
        private readonly IViewEngine _viewEngine;

        private static IEnumerable<EntityDescriptionDto>? entityDescriptionsCache = null;
        /// <summary>
        /// 代码生成服务
        /// </summary>
        /// <param name="entityConfigService"></param>
        /// <param name="viewEngine"></param>
        /// <param name="fieldConfigService"></param>
        public CodeGenerationService(IEntityConfigService entityConfigService, IViewEngine viewEngine, IFieldConfigService fieldConfigService)
        {
            _entityConfigService = entityConfigService;
            _viewEngine = viewEngine;
            _fieldConfigService = fieldConfigService;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <remarks>
        /// 根据模板和实体类信息生成代码
        /// </remarks>
        /// <param name="generateCodeInput"></param>
        /// <returns></returns>
        public async Task<string> GenerationCode(GenerateCodeInput generateCodeInput)
        {
            var entityDescriptions = await GetEntityDescriptions();
            var entityDescription = entityDescriptions.FirstOrDefault(x => x.EntityTypeFullName.Equals(generateCodeInput.EntityTypeFullName));
            if (entityDescription == null)
            {
                throw Oops.BahLocalFrom<CodeGenLocalResource>(nameof(CodeGenLocalResource.EntityTypeIsNotFind), generateCodeInput.EntityTypeFullName);
            }
            //配置
            try
            {
                string code = await _viewEngine.RunCompileAsync(generateCodeInput.TemplateContent, entityDescription, builderAction: builder =>
                {
                    builder.AddAssemblyReferenceByName("System");
                    builder.AddAssemblyReferenceByName("Gardener.Core");
                    builder.AddAssemblyReferenceByName("Gardener.Core.Util");
                    builder.AddAssemblyReferenceByName("Gardener.Core.Shared");

                    builder.AddUsing("System");
                    builder.AddUsing("Gardener.Core");
                    builder.AddUsing("Gardener.Core.Resources");
                    builder.AddUsing("Gardener.Core.Enums");
                    builder.AddUsing("Gardener.Core.Attributes");
                    builder.AddUsing("Gardener.Core.Dtos");
                    builder.AddUsing("Gardener.Core.Util");
                    builder.AddUsing("Gardener.Core.Util.Extensions");
                });
                return code;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }

        /// <summary>
        /// 获取实体描述列表
        /// </summary>
        /// <remarks>
        /// 获取所有实体描述列表
        /// </remarks>
        /// <returns></returns>
        public async Task<IEnumerable<EntityDescriptionDto>> GetEntityDescriptions()
        {
            if (entityDescriptionsCache == null)
            {
                entityDescriptionsCache = Scan();
            }
            IEnumerable<EntityDescriptionDto> Scan()
            {
                List<Type> entityTypes = new List<Type>();

                foreach (var item in App.Assemblies)
                {
                    var types = item.GetTypes();
                    foreach (var type in types)
                    {
                        if (!type.IsAssignableTo(typeof(IPrivateEntity)) || type.IsAbstract) continue;
                        entityTypes.Add(type);
                    }
                }


                List<EntityDescriptionDto> result = new List<EntityDescriptionDto>();
                foreach (var item in entityTypes)
                {
                    List<FieldDescriptionDto> fields = GetFieldDescriptions(item);
                    DisplayAttribute? displayAttribute = item.GetAttribute<DisplayAttribute>();
                    string entityDtoName = item.Name + "Dto";
                    string? baseTypeName = item.BaseType?.Name;

                    if (baseTypeName != null && !baseTypeName.Contains("EntityBase"))
                    {
                        entityDtoName = baseTypeName;
                    }
                    var entityDesc = new EntityDescriptionDto()
                    {
                        EntityType = item,
                        EntityDtoName = entityDtoName,
                        EntityTypeName = item.Name,
                        EntityTypeFullName = item.FullName ?? "",
                        NameSpace = item.Namespace ?? "",
                        AssemblyName = item.Assembly.ManifestModule.Name ?? "",
                        ImplementIModelCreated = item.IsAssignableTo(typeof(IModelCreated)),
                        ImplementIModelLocked = item.IsAssignableTo(typeof(IModelLocked)),
                        ImplementIModelDeleted = item.IsAssignableTo(typeof(IModelDeleted)),
                        ImplementIModelUpdated = item.IsAssignableTo(typeof(IModelUpdated)),
                        ImplementIModelTenant = item.IsAssignableTo(typeof(IModelTenant)),
                        ImplementIModelTenantId = item.IsAssignableTo(typeof(IModelTenantId)),
                        ImplementIModelModule = item.IsAssignableTo(typeof(IModelModule)),
                        Fields = fields,
                        DisplayName = displayAttribute?.GetName(),
                        DisplayNameResourceTypeName = displayAttribute?.ResourceType?.Name,
                        DisplayNameResourceType = displayAttribute?.ResourceType
                    };
                    var primaryKey = fields.FirstOrDefault(x => x.IsPrimaryKey);
                    if (primaryKey != null)
                    {
                        Type genericType = typeof(IModelId<>);
                        Type[] typeArguments = new Type[] { primaryKey.Type };
                        Type constructedType = genericType.MakeGenericType(typeArguments);
                        entityDesc.ImplementIModelId = item.IsAssignableTo(constructedType);
                        entityDesc.PrimaryKeyName = primaryKey.Name;
                        entityDesc.PrimaryKeyTypeName = primaryKey.TypeName;
                    }
                    result.Add(entityDesc);
                }
                return result;

            }
            var configs = await _entityConfigService.GetAll();
            var fieldConfigs = await _fieldConfigService.GetAll();
            foreach (var entityDesc in entityDescriptionsCache)
            {
                var entityConfig = configs.FirstOrDefault(x => x.Id.Equals(entityDesc.EntityTypeFullName));
                if (entityConfig == null)
                {
                    string baseNameSpace = entityDesc.AssemblyName.Replace(".Api.", ".").Replace(".Impl", "").Replace(".Entities", "").Replace(".dll", "");
                    entityConfig = new EntityConfigDto()
                    {
                        Id = entityDesc.EntityTypeFullName,
                        BaseNameSpace = baseNameSpace,
                        ApiNameSpace = baseNameSpace + ".Impl",
                        ClientNameSpace = baseNameSpace + ".Client",
                        MenuName = entityDesc.DisplayName,
                        EnableAdd = true,
                        EnableDelete = true,
                        EnableUpdate = true,
                        EnableDeleteSelected = true,
                        EnableDetail = true,
                        EnableLock = true,
                        EnableRefresh = true,
                        EnableSearch = true,
                        UseTreeList = false,
                    };

                }
                entityDesc.EntityConfig = entityConfig;
                int minOrder = 100;
                int orderStep = 10;
                int orderIndex = 0;
                entityDesc.Fields.ForEach(x =>
                {
                    x.FieldConfig.ListOrder = minOrder + (orderIndex * orderStep);
                    orderIndex++;
                    if (!x.IsPrimaryKey
                    && !x.Name.Equals(nameof(IModelDeleted.IsDeleted))
                    && !x.Name.Equals(nameof(IModelCreated.CreatedTime))
                    && !x.Name.Equals(nameof(IModelCreated.CreateIdentityType))
                    && !x.Name.Equals(nameof(IModelCreated.CreateBy))
                    && !x.Name.Equals(nameof(IModelUpdated.UpdatedTime))
                    && !x.Name.Equals(nameof(IModelUpdated.UpdateIdentityType))
                    && !x.Name.Equals(nameof(IModelUpdated.UpdateBy))
                    && !x.Name.Equals(nameof(IModelTenantPermission.EmpowerAllTenants))
                    )
                    {
                        x.FieldConfig.CanModity = true;
                    }

                    if (x.Name.Equals(nameof(IModelDeleted.IsDeleted)) ||
                    x.Name.Equals(nameof(IModelUpdated.UpdateBy)) ||
                    x.Name.Equals(nameof(IModelUpdated.UpdateIdentityType)) ||
                    x.Name.Equals(nameof(IModelTenantPermission.EmpowerAllTenants)) ||
                    x.Name.Equals(nameof(IModelCreated.CreateBy)) ||
                    x.Name.Equals(nameof(IModelCreated.CreateIdentityType)))
                    {
                        x.FieldConfig.ShowInList = false;
                        x.FieldConfig.ShowInDetail = false;
                        x.FieldConfig.ListOrder = minOrder + 10000;
                    }
                    else if (x.Name.Equals(nameof(IModelId<int>.Id)))
                    {
                        x.FieldConfig.ListOrder = 0;
                    }
                    else if (x.Name.Equals(nameof(IModelTenantId.TenantId)))
                    {
                        x.FieldConfig.ListOrder = 1;
                    }
                    else if (x.Name.Equals(nameof(IModelLocked.IsLocked)))
                    {
                        x.FieldConfig.ListOrder = 2000;
                    }
                    else if (x.Name.Equals(nameof(IModelCreated.CreatedTime)))
                    {
                        x.FieldConfig.Sortable = true;
                        x.FieldConfig.DefaultSortOrder = "desc";
                        x.FieldConfig.ListOrder = 2010;
                    }
                    else if (x.Name.Equals(nameof(IModelUpdated.UpdatedTime)))
                    {
                        x.FieldConfig.ListOrder = 2020;
                    }
                    x.FieldConfig.DetailOrder = x.FieldConfig.ListOrder;
                    var fieldConfig = fieldConfigs.FirstOrDefault(y => y.EntityTypeFullName.Equals(entityDesc.EntityTypeFullName) && y.FieldName.Equals(x.Name));
                    if (fieldConfig != null)
                    {
                        x.FieldConfig = fieldConfig;
                    }
                });
            }
            return entityDescriptionsCache;

        }

        /// <summary>
        /// 获取字段描述信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<FieldDescriptionDto> GetFieldDescriptions(Type type)
        {
            List<FieldDescriptionDto> fields = new List<FieldDescriptionDto>();
            var ps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in ps)
            {
                Type pType = p.PropertyType;
                bool isNullableType = pType.IsNullableType();
                if (isNullableType)
                {
                    pType = pType.GetUnNullableType();
                }
                if (!pType.IsSystemType())
                {
                    continue;
                }
                //字符串类型获取类型不包含Nullable信息
                int? maxLength = null;
                int? minLength = null;
                if (pType.Equals(typeof(String)))
                {
                    isNullableType = !p.HasAttribute<RequiredAttribute>();
                    maxLength = p.GetAttribute<MaxLengthAttribute>()?.Length;
                    minLength = p.GetAttribute<MinLengthAttribute>()?.Length;
                }
                bool isEnum = pType.IsEnum;
                var fd = new FieldDescriptionDto(type.FullName ?? type.Name, p.Name, pType.Name);
                fd.Type = pType;
                fd.IsNullableType = isNullableType;
                fd.IsEnum = isEnum;
                fd.IsPrimaryKey = p.HasAttribute<KeyAttribute>();
                fd.Order = p.GetAttribute<OrderAttribute>()?.Order;
                fd.DictCodeTypeValue = p.GetAttribute<CodeTypeAttribute>()?.CodeTypeValue;
                if (fd.DictCodeTypeValue != null)
                {
                    fd.IsDictCodeField = true;
                }
                fd.MaxLength = maxLength;
                fd.MinLength = minLength;
                fields.Add(fd);
            }

            return fields;
        }
    }
}
