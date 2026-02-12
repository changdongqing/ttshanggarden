// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Gardener.Core.Client.Components.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public class UniqueValueValidationDescription<TDto>
    {
        /// <summary>
        /// 目前只能提前设置校验值
        /// （因不支持异步）
        /// </summary>
        private bool _isValidOnSubmitBefor = true;
        /// <summary>
        /// 唯一值验证结果
        /// </summary>
        private ClientListBindValue<string, bool> _uniqueVerificationStates;
        /// <summary>
        /// 唯一值验证字段值提供者
        /// </summary>
        private ClientListBindValue<string, Func<TDto, object?>> _uniqueVerificationValueProviders;
        /// <summary>
        /// 需要排除的字段（如编辑时的Id）
        /// </summary>
        private ClientListBindValue<string, Func<TDto, object?>> _uniqueVerificationExcludeFieldProviders;
        /// <summary>
        /// 组合唯一值
        /// </summary>

        private Dictionary<string, Dictionary<string, Func<TDto, object?>>> fieldCombinations = new Dictionary<string, Dictionary<string, Func<TDto, object?>>>();
        /// <summary>
        /// 
        /// </summary>
        public UniqueValueValidationDescription()
        {
            _uniqueVerificationStates = new ClientListBindValue<string, bool>(true);
            _uniqueVerificationValueProviders = new ClientListBindValue<string, Func<TDto, object?>>(x => null);
            _uniqueVerificationExcludeFieldProviders = new ClientListBindValue<string, Func<TDto, object?>>(x => null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string? GetFieldName(Expression<Func<TDto, object?>> expression)
        {

            string? fieldName = null;

            if (expression.Body is MemberExpression memberExp)
            {
                fieldName = memberExp.Member.Name;
            }
            else if (expression.Body is UnaryExpression memberExp1)
            {
                fieldName = ((MemberExpression)memberExp1.Operand).Member.Name;
            }
            else if (expression.Body is ParameterExpression memberExp2)
            {
                fieldName = memberExp2.Type.Name;
            }
            return fieldName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public UniqueValueValidationDescription<TDto> ExcludeField(Expression<Func<TDto, object?>> expression)
        {
            string? fieldName = GetFieldName(expression);

            if (fieldName != null)
            {
                _uniqueVerificationExcludeFieldProviders[fieldName] = expression.Compile();
            }
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public UniqueValueValidationDescription<TDto> AddField(Expression<Func<TDto, object?>> expression, params Expression<Func<TDto, object?>>[] combinations)
        {
            string? fieldName = GetFieldName(expression);

            if (fieldName != null)
            {
                _uniqueVerificationValueProviders[fieldName] = expression.Compile();

                if (combinations != null)
                {
                    //组合唯一
                    if (fieldCombinations.ContainsKey(fieldName))
                    {
                        fieldCombinations.Remove(fieldName);
                    }
                    var dic = new Dictionary<string, Func<TDto, object?>>(combinations.Length);
                    foreach (var item in combinations)
                    {
                        string? fieldNameTemp = GetFieldName(item);
                        if (fieldNameTemp == null)
                        {
                            continue;
                        }
                        dic.Add(fieldNameTemp, item.Compile());
                    }
                    fieldCombinations.Add(fieldName, dic);
                }
            }

            return this;
        }
        private Func<TDto>? _dtoPr;
        private Func<List<FilterGroup>, Task<bool>>? _existsFun;
        public UniqueValueValidationDescription<TDto> SetModelProviders(Func<TDto> dtoPr)
        {
            _dtoPr = dtoPr;
            return this;
        }
        public UniqueValueValidationDescription<TDto> SetExistsFun(Func<List<FilterGroup>, Task<bool>> existsFun)
        {
            _existsFun = existsFun;
            return this;
        }
        /// <summary>
        /// 是否唯一
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public (bool, string[]?) IsUnique(string fieldName)
        {
            if (_isValidOnSubmitBefor)
            {
                string[]? combFields = null;
                if (fieldCombinations.TryGetValue(fieldName, out Dictionary<string, Func<TDto, object?>>? comb))
                {
                    if (comb != null)
                    {
                        combFields = comb.Keys.ToArray();
                    }
                }
                return (_uniqueVerificationStates[fieldName], combFields);
            }
            if (_dtoPr == null || _existsFun == null)
            {
                return (false, null);
            }
            return (false, null);
            //TDto dto = _dtoPr();
            ////获取字段值
            //object? value = _uniqueVerificationValueProviders[fieldName].Invoke(dto);
            //if (value == null)
            //{
            //    return (true, null);
            //}
            //List<FilterRule> filterRules = [
            //                new FilterRule(fieldName, value, FilterOperate.Equal),
            //            ];
            //IDictionary<string, Func<TDto, object?>> excludeFields = GetExcludeFields();
            //foreach (var item in excludeFields)
            //{
            //    var itemValue = item.Value.Invoke(dto);
            //    if (itemValue == null) continue;
            //    filterRules.Add(new FilterRule(item.Key, itemValue, FilterOperate.NotEqual));
            //}
            //var exists = _existsFun([new FilterGroup(filterRules)]);
            //return (!exists.Result, null);
        }
        /// <summary>
        /// 设置是否唯一
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="isUnique"></param>
        public void SetState(string fieldName, bool isUnique)
        {
            _uniqueVerificationStates[fieldName] = isUnique;
        }

        /// <summary>
        /// 获取已添加字段
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<string, Func<TDto, object?>> GetFields()
        {
            return _uniqueVerificationValueProviders.GetValues();
        }

        /// <summary>
        /// 获取需要排除的字段
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<string, Func<TDto, object?>> GetExcludeFields()
        {
            return _uniqueVerificationExcludeFieldProviders.GetValues();
        }

        /// <summary>
        /// 检查所有字段
        /// </summary>
        /// <returns></returns>
        public async Task CheckAllFields()
        {
            if (_dtoPr == null || _existsFun == null)
            {
                return;
            }
            TDto _editModel = _dtoPr();
            List<(string, Task<bool>)> tasks = new List<(string, Task<bool>)>();
            //校验唯一值
            foreach (var field in GetFields())
            {
                object? value = field.Value.Invoke(_editModel);
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str))) continue;
                List<FilterRule> filterRules = [
                                new FilterRule(field.Key, value, FilterOperate.Equal),
                        ];
                if (fieldCombinations.TryGetValue(field.Key, out Dictionary<string, Func<TDto, object?>>? comb))
                {
                    if (comb != null)
                    {
                        foreach (var item in comb)
                        {
                            var itemValue = item.Value.Invoke(_editModel);
                            //if (itemValue == null || (itemValue is string str2 && string.IsNullOrWhiteSpace(str2))) continue;
                            filterRules.Add(new FilterRule(item.Key, itemValue, FilterOperate.Equal));
                        }
                    }
                }
                IDictionary<string, Func<TDto, object?>> excludeFields = GetExcludeFields();
                foreach (var item in excludeFields)
                {
                    var itemValue = item.Value.Invoke(_editModel);
                    if (itemValue == null) continue;
                    filterRules.Add(new FilterRule(item.Key, itemValue, FilterOperate.NotEqual));
                }
                var existsTask = _existsFun([new FilterGroup(filterRules)]);
                tasks.Add((field.Key, existsTask));
            }
            await Task.WhenAll(tasks.Select(x => x.Item2));
            tasks.ForEach(x =>
            {
                //存在，不唯一
                SetState(x.Item1, !x.Item2.Result);
            });
        }
    }
}
