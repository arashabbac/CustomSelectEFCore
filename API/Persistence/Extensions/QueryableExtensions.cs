using System.Linq.Expressions;

namespace Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SelectCustomColumns<T>(this IQueryable<T> source, string[] columnNames)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "current");

                var bindings = columnNames
                    .Select(name => Expression.PropertyOrField(parameter, name))
                    .Select(member => Expression.Bind(member.Member, member));

                var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
                var selector = Expression.Lambda<Func<T, T>>(body, parameter);
                return source.Select(selector);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(message: "Selected column(s) are not existed!", innerException: ex);
            }
        }
    }
}