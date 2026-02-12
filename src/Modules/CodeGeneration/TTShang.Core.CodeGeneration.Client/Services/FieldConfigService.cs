namespace TTShang.Core.CodeGeneration.Client.Services
{
    /// <summary>
    /// 字段配置服务
    /// </summary>
    [ScopedService]
    public class FieldConfigService : ClientServiceBase<FieldConfigDto, int>, IFieldConfigService
    {
        /// <summary>
        /// 字段配置服务
        /// </summary>
        public FieldConfigService(IApiCaller apiCaller) : base(apiCaller, "field-config", "code-gen")
        {
        }

        public Task<List<FieldConfigDto>> FindByEntityTypeFullName(string entityTypeFullName)
        {
            IDictionary<string, object?> keyValues = new Dictionary<string, object?>();
            keyValues.Add(nameof(entityTypeFullName), entityTypeFullName);
            return apiCaller.GetAsync<List<FieldConfigDto>>($"{base.baseUrl}/find-by-entity-type-full-name", keyValues);
        }
    }
}
