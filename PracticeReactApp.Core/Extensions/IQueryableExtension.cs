using System.Linq.Expressions;
using System.Transactions;

namespace PracticeReactApp.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public enum Order
        {
            Asc,
            Desc
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Order direction)
        {
            return direction == Order.Desc ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, Order direction)
        {
            if (string.IsNullOrEmpty(orderByMember)) return query;

            var orderByMembers = orderByMember.Split(',').ToList();

            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var isFirstOrder = true;

            foreach (var member in orderByMembers)
            {
                var memberAccess = Expression.PropertyOrField(queryElementTypeParam, member);
                var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);
                var orderBy = Expression.Call(
                    typeof(Queryable),
                    GetOrderDirection(direction, isFirstOrder),
                    new Type[] { typeof(T), memberAccess.Type },
                    query.Expression,
                    Expression.Quote(keySelector));

                query = query.Provider.CreateQuery<T>(orderBy);

                isFirstOrder = false;
            }
            
            return query;
        }

        private static string GetOrderDirection(Order direction, bool isFirstOrder)
        {
            if (direction == Order.Asc) return (isFirstOrder) ? "OrderBy" : "ThenBy";
            else return (isFirstOrder) ? "OrderByDescending" : "ThenByDescending";
        }

        public static List<T> ToListWithNoLock<T>(this IQueryable<T> query)
        {
            List<T>? result = null;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = query.ToList();
                scope.Complete();
            }
            return result;
        }
    }
}