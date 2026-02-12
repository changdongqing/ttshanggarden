// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Linq.Expressions;
using System.Reflection;

namespace TTShang.Core.Util
{
    /// <summary>
    /// 表达式工具
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MemberInfo? GetReturnMemberInfo(LambdaExpression expression)
        {
            var accessorBody = expression.Body;
            while (true)
            {
                if (accessorBody is UnaryExpression unaryExpression)
                {
                    accessorBody = unaryExpression.Operand;
                }
                else if (accessorBody is ConditionalExpression conditionalExpression)
                {
                    accessorBody = conditionalExpression.IfTrue;
                }
                else if (accessorBody is MethodCallExpression methodCallExpression)
                {
                    accessorBody = methodCallExpression.Object;
                }
                else if (accessorBody is BinaryExpression binaryExpression)
                {
                    accessorBody = binaryExpression.Left;
                }
                else
                {
                    break;
                }
            }

            if (accessorBody is not MemberExpression memberExpression)
            {
                return null;
            }

            return memberExpression.Member;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetFieldName<TEntity>(Expression<Func<TEntity, object?>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operandMemberExpression)
            {
                return operandMemberExpression.Member.Name;
            }
            throw new ArgumentException("表达式不表示对成员的访问。");
        }
    }
}
