namespace Gardener.Core.Attributes
{
    /// <summary>
    /// guid不为空验证
    /// </summary>
    public class GuidRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            return value != null && !Guid.Empty.Equals(value);
        }
    }
}
