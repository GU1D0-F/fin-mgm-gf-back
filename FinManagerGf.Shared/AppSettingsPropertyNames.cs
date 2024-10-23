using System.Linq.Expressions;

namespace FinManagerGf.Shared
{
    public static class AppSettingsPropertyNames
    {
        public static readonly string DefaultConnection = GetPropertyName(() => DefaultConnection);
        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("Expression is not a member expression.");
        }
    }
}
