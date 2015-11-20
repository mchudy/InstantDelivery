using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace InstantDelivery.Domain.Extensions
{
    /// <summary>
    /// Metody rozszerzające klasy IQuerable
    /// </summary>
    public static class IQueryableExtensions
    {
        private static readonly MethodInfo OrderByMethod = typeof(Queryable)
            .GetMethods()
            .Where(method => method.Name == "OrderBy")
            .Single(method => method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable)
            .GetMethods()
            .Where(method => method.Name == "OrderByDescending")
            .Single(method => method.GetParameters().Length == 2);

        public static IList<T> Page<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public static IQueryable<TSource> OrderByProperty<TSource>
            (this IQueryable<TSource> source, string propertyName)
        {
            LambdaExpression lambda;
            var orderByProperty = GetOrderByExpression<TSource>(propertyName, out lambda);
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(
                typeof(TSource), orderByProperty.Type);
            return GetSortedSource(source, genericMethod, lambda);
        }

        public static IQueryable<TSource> OrderByDescendingProperty<TSource>
            (this IQueryable<TSource> source, string propertyName)
        {
            LambdaExpression lambda;
            var orderByProperty = GetOrderByExpression<TSource>(propertyName, out lambda);
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(
                typeof(TSource), orderByProperty.Type);
            return GetSortedSource(source, genericMethod, lambda);
        }

        private static IQueryable<TSource> GetSortedSource<TSource>(IQueryable<TSource> source, MethodInfo genericMethod, LambdaExpression lambda)
        {
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }

        private static Expression GetOrderByExpression<TSource>(string propertyName, out LambdaExpression lambda)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "posting");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            lambda = Expression.Lambda(orderByProperty, parameter);
            return orderByProperty;
        }
    }

}
