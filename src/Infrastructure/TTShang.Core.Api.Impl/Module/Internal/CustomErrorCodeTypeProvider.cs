// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.Api.Impl.Module.Internal
{
    /// <summary>
    /// 异常错误码提供器
    /// </summary>
    /// <remarks>
    /// 从各模块中读取注册的错误码Type。
    /// 类型中的字段可以作为错误码。
    /// 常用枚举类型。
    /// </remarks>
    public class CustomErrorCodeTypeProvider : IErrorCodeTypeProvider
    {
        private readonly ServerModuleManager moduleManager;

        /// <summary>
        /// 异常错误码提供器
        /// </summary>
        /// <param name="moduleManager"></param>
        public CustomErrorCodeTypeProvider(ServerModuleManager moduleManager)
        {
            this.moduleManager = moduleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type[] Definitions
        {
            get
            {
                List<Type> result = new List<Type>();
                IEnumerable<IServerModule> modules = moduleManager.GetServerModules();
                foreach (IServerModule module in modules)
                {
                    var types = module.GetCustomErrorCodeTypes();
                    if (types != null)
                    {
                        result.AddRange(types);
                    }
                }
                return result.Distinct().ToArray();
            }
        }
    }
}
